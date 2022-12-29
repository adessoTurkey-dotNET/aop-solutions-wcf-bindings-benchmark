# Benchmark Projects for 2022H2 OKR

## Benchmark of Solutions to Different Problems Using Different Aspect Oriented Techniques

You can run the project using these commands at the project root (/AOP/);
> docker run -p 6379:6379 --name aop-redis -d redis redis-server --save 20 1 --loglevel warning --requirepass kZPhV5fS  
> dotnet run -c RELEASE

To understand a overhead affects of different solutions using aspect oriented programming techniques, I made a simple benchmark project. At this article I want to share the results with you.

First of all we need something to simulate time-consuming business logic. For this, I have defined a simple Fibonacci calculator service whose interface is specified below.

```csharp
public interface IFibonacciService
{
    ulong Calculate(int n, bool optimized);
}
```

I have implemented two different implementations of this service, optimized and non-optimized, as follows;

```csharp
public class FibonacciService : IFibonacciService
{
    private readonly Dictionary<int, ulong> _cache = new Dictionary<int, ulong>();

    private ulong CalculateWithoutCache(int n)
    {
        n = n < 0 ? 0 : n;

        if (n < 2)
            return (ulong)n;

        _cache[n] = CalculateWithoutCache(n - 1) + CalculateWithoutCache(n - 2);
        return _cache[n];
    }

    private ulong CalculateWithCache(int n)
    {
        n = n < 0 ? 0 : n;

        if (n < 2)
            return (ulong)n;

        if (_cache.ContainsKey(n))
        {
            return _cache[n];
        }

        _cache[n] = CalculateWithCache(n - 1) + CalculateWithCache(n - 2);
        return _cache[n];
    }

    public ulong Calculate(int n, bool optimized)
    {
        if (optimized)
        {
            return CalculateWithCache(n);
        }

        return CalculateWithoutCache(n);
    }
}
```
By using this class to simulate business logic, we address three main problems that we encounter in daily life, which are;

- Profiling
- Logging
- Caching

We will solve these problems using two different methods using aspect oriented programming techniques and focus performance difference between two of them.

Let's start with solutions using **proxy design pattern** implementations;

```csharp
public class ProfiledFibonacciServiceProxy : IFibonacciService
{
    private readonly FibonacciService _service;

    public ProfiledFibonacciServiceProxy()
    {
        _service = new FibonacciService();
    }

    public ulong Calculate(int n, bool optimized)
    {
        var watch = Stopwatch.StartNew();
        var res = _service.Calculate(n, optimized);
        Console.WriteLine($"Service time: {watch.ElapsedMilliseconds}ms");
        return res;
    }
}
```

```csharp
public class LoggedFibonacciServiceProxy : IFibonacciService
{
    private readonly FibonacciService _service;
    private readonly ILogger _logger;

    public LoggedFibonacciServiceProxy()
    {
        _service = new FibonacciService();
        _logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<LoggedFibonacciServiceProxy>();
    }

    public ulong Calculate(int n, bool optimized)
    {
        var res = _service.Calculate(n, optimized);
        _logger.LogTrace(res.ToString());
        return res;
    }
}
```

```csharp
public class CachedFibonacciServiceProxy : IFibonacciService
{
    private readonly FibonacciService _service;

    public CachedFibonacciServiceProxy()
    {
        _service = new FibonacciService();
    }

    public ulong Calculate(int n, bool optimized)
    {
        var cachedValue = RedisService.db.StringGet(n.ToString());
        if (cachedValue.IsNullOrEmpty)
        {
            var res = _service.Calculate(n, optimized);
            RedisService.db.StringSet(n.ToString(), res.ToString());
            return res;
        }

        return Convert.ToUInt64(cachedValue);
    }
}
```

Below are benchmark results of these solutions;

|                        Method |  n | optimized |          Mean |       Error |       StdDev |         Median |             Min |          Max | Iterations |
|------------------------------ |--- |---------- |--------------:|------------:|-------------:|---------------:|----------------:|-------------:|-----------:|
|                       Default | 34 |     False | 146,239.04 us | 5,530.29 us | 16,306.19 us | 142,320.500 us | 132,026.2000 us | 259,289.7 us |      100.0 |
|       ProfileWithProxyPattern | 34 |     False | 140,877.82 us | 5,520.59 us | 16,277.57 us | 137,663.300 us | 130,982.2000 us | 286,175.7 us |      100.0 |
|         CacheWithProxyPattern | 34 |     False |   3,247.02 us | 5,176.11 us | 15,261.87 us |   1,707.200 us |   1,279.5000 us | 154,320.2 us |      100.0 |
|           LogWithProxyPattern | 34 |     False | 140,933.69 us | 6,298.99 us | 18,572.70 us | 136,057.050 us | 131,917.3000 us | 299,852.9 us |      100.0 |

If we use optimized calculation method, it's a whole different story and we can clearly see the overhead caused by aop techniques if we compare them with the default;

|                        Method |  n | optimized |          Mean |       Error |       StdDev |         Median |             Min |          Max | Iterations |
|------------------------------ |--- |---------- |--------------:|------------:|-------------:|---------------:|----------------:|-------------:|-----------:|
|                       Default | 34 |      True |      23.63 us |    72.46 us |    213.64 us |       1.900 us |       0.4000 us |   2,138.4 us |      100.0 |
|       ProfileWithProxyPattern | 34 |      True |      49.25 us |    93.22 us |    274.86 us |      20.400 us |       9.5000 us |   2,768.1 us |      100.0 |
|         CacheWithProxyPattern | 34 |      True |   3,038.50 us | 4,683.52 us | 13,809.46 us |   1,513.450 us |   1,312.7000 us | 139,623.8 us |      100.0 |
|           LogWithProxyPattern | 34 |      True |     282.03 us |   717.33 us |  2,115.05 us |      54.550 us |      23.1000 us |  21,167.1 us |      100.0 |

Now, lets use Castle.DynamicProxy library to implement solutions;

```csharp
public class ProfiledFibonacciServiceInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var watch = Stopwatch.StartNew();
        try
        {
            invocation.Proceed();
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Target exception {e.Message}");
            throw;
        }
        finally
        {
            Debug.WriteLine($"Service time: {watch.ElapsedMilliseconds}ms");
        }
    }
}
```

```csharp
public class LoggedFibonacciServiceInterceptor : IInterceptor
{
    private readonly ILogger _logger;

    public LoggedFibonacciServiceInterceptor()
    {
        _logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<LoggedFibonacciServiceInterceptor>();
    }

    public void Intercept(IInvocation invocation)
    {
        try
        {
            invocation.Proceed();
            _logger.LogTrace(invocation.ReturnValue.ToString());
        }
        catch (Exception e)
        {
            _logger.LogError($"Target exception {e.Message}");
            throw;
        }
    }
}
```

```csharp
public class CachedFibonacciServiceInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        try
        {
            var cachedValue = RedisService.db.StringGet(invocation.GetArgumentValue(0).ToString());
            if (cachedValue.IsNullOrEmpty)
            {
                invocation.Proceed();
                RedisService.db.StringSet(invocation.GetArgumentValue(0).ToString(), invocation.ReturnValue.ToString());;
            }

            invocation.ReturnValue = Convert.ToUInt64(cachedValue);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Target exception {e.Message}");
            throw;
        }
    }
}
```

Below are results of these solutions using Castle.DynamicProxy library;

|                        Method |  n | optimized |          Mean |       Error |       StdDev |         Median |             Min |          Max | Iterations |
|------------------------------ |--- |---------- |--------------:|------------:|-------------:|---------------:|----------------:|-------------:|-----------:|
|                       Default | 34 |     False | 146,239.04 us | 5,530.29 us | 16,306.19 us | 142,320.500 us | 132,026.2000 us | 259,289.7 us |      100.0 |
| ProfileWithCastleDynamicProxy | 34 |     False | 138,728.02 us | 5,507.41 us | 16,238.72 us | 134,883.350 us | 131,095.2000 us | 280,761.9 us |      100.0 |
|   CacheWithCastleDynamicProxy | 34 |     False |   2,852.98 us | 4,582.88 us | 13,512.74 us |   1,485.100 us |   1,230.4000 us | 136,620.1 us |      100.0 |
|     LogWithCastleDynamicProxy | 34 |     False | 140,049.44 us | 5,969.54 us | 17,601.34 us | 136,255.750 us | 131,423.4000 us | 298,510.1 us |      100.0 |
|                       Default | 34 |      True |      23.63 us |    72.46 us |    213.64 us |       1.900 us |       0.4000 us |   2,138.4 us |      100.0 |
| ProfileWithCastleDynamicProxy | 34 |      True |      29.33 us |    83.26 us |    245.50 us |       4.200 us |       2.0000 us |   2,459.1 us |      100.0 |
|   CacheWithCastleDynamicProxy | 34 |      True |   2,843.39 us | 4,398.34 us | 12,968.59 us |   1,522.500 us |   1,250.5000 us | 131,218.4 us |      100.0 |
|     LogWithCastleDynamicProxy | 34 |      True |     284.93 us |   722.08 us |  2,129.08 us |      56.900 us |      25.3000 us |  21,316.8 us |      100.0 |

Now lets compare each solution one by one using optimized calculation method.

First, profiling solutions;

|                        Method |  n | optimized |          Mean |       Error |       StdDev |         Median |             Min |          Max | Iterations |
|------------------------------ |--- |---------- |--------------:|------------:|-------------:|---------------:|----------------:|-------------:|-----------:|
|                       Default | 34 |      True |      23.63 us |    72.46 us |    213.64 us |       1.900 us |       0.4000 us |   2,138.4 us |      100.0 |
|       ProfileWithProxyPattern | 34 |      True |      49.25 us |    93.22 us |    274.86 us |      20.400 us |       9.5000 us |   2,768.1 us |      100.0 |
| ProfileWithCastleDynamicProxy | 34 |      True |      29.33 us |    83.26 us |    245.50 us |       4.200 us |       2.0000 us |   2,459.1 us |      100.0 |

DynamicProxy is a clear winner here.

Second, caching solutions;

|                        Method |  n | optimized |          Mean |       Error |       StdDev |         Median |             Min |          Max | Iterations |
|------------------------------ |--- |---------- |--------------:|------------:|-------------:|---------------:|----------------:|-------------:|-----------:|
|                       Default | 34 |      True |      23.63 us |    72.46 us |    213.64 us |       1.900 us |       0.4000 us |   2,138.4 us |      100.0 |
|         CacheWithProxyPattern | 34 |      True |   3,038.50 us | 4,683.52 us | 13,809.46 us |   1,513.450 us |   1,312.7000 us | 139,623.8 us |      100.0 |
|   CacheWithCastleDynamicProxy | 34 |      True |   2,843.39 us | 4,398.34 us | 12,968.59 us |   1,522.500 us |   1,250.5000 us | 131,218.4 us |      100.0 |

Notice the big difference between Default and our solutions. This is mainly caused by Redis usage.
So, if we compare the solutions, it seems that DynamicProxy has slightly better performance, but that's not much of a difference and may be within the margin of error.

Finally, logging solutions;

|                        Method |  n | optimized |          Mean |       Error |       StdDev |         Median |             Min |          Max | Iterations |
|------------------------------ |--- |---------- |--------------:|------------:|-------------:|---------------:|----------------:|-------------:|-----------:|
|                       Default | 34 |      True |      23.63 us |    72.46 us |    213.64 us |       1.900 us |       0.4000 us |   2,138.4 us |      100.0 |
|           LogWithProxyPattern | 34 |      True |     282.03 us |   717.33 us |  2,115.05 us |      54.550 us |      23.1000 us |  21,167.1 us |      100.0 |
|     LogWithCastleDynamicProxy | 34 |      True |     284.93 us |   722.08 us |  2,129.08 us |      56.900 us |      25.3000 us |  21,316.8 us |      100.0 |

Not much of a difference here either.

### Summary

Even though DynamicProxy library performed better to solve profiling (to console), there are not much of a difference with proxy design pattern when it comes to more complex solutions which uses other components. (redis, file i/o etc.)

So, in the end, I think it comes down to the personal preference whatever using Castle.DynamicProxy library or implementing your own solution using Proxy Design Pattern.

### Complete Result Set
|                        Method |  n | optimized |          Mean |       Error |       StdDev |         Median |             Min |          Max | Iterations |
|------------------------------ |--- |---------- |--------------:|------------:|-------------:|---------------:|----------------:|-------------:|-----------:|
|                       Default | 34 |     False | 146,239.04 us | 5,530.29 us | 16,306.19 us | 142,320.500 us | 132,026.2000 us | 259,289.7 us |      100.0 |
|       ProfileWithProxyPattern | 34 |     False | 140,877.82 us | 5,520.59 us | 16,277.57 us | 137,663.300 us | 130,982.2000 us | 286,175.7 us |      100.0 |
|         CacheWithProxyPattern | 34 |     False |   3,247.02 us | 5,176.11 us | 15,261.87 us |   1,707.200 us |   1,279.5000 us | 154,320.2 us |      100.0 |
|           LogWithProxyPattern | 34 |     False | 140,933.69 us | 6,298.99 us | 18,572.70 us | 136,057.050 us | 131,917.3000 us | 299,852.9 us |      100.0 |
| ProfileWithCastleDynamicProxy | 34 |     False | 138,728.02 us | 5,507.41 us | 16,238.72 us | 134,883.350 us | 131,095.2000 us | 280,761.9 us |      100.0 |
|   CacheWithCastleDynamicProxy | 34 |     False |   2,852.98 us | 4,582.88 us | 13,512.74 us |   1,485.100 us |   1,230.4000 us | 136,620.1 us |      100.0 |
|     LogWithCastleDynamicProxy | 34 |     False | 140,049.44 us | 5,969.54 us | 17,601.34 us | 136,255.750 us | 131,423.4000 us | 298,510.1 us |      100.0 |
|                       Default | 34 |      True |      23.63 us |    72.46 us |    213.64 us |       1.900 us |       0.4000 us |   2,138.4 us |      100.0 |
|       ProfileWithProxyPattern | 34 |      True |      49.25 us |    93.22 us |    274.86 us |      20.400 us |       9.5000 us |   2,768.1 us |      100.0 |
|         CacheWithProxyPattern | 34 |      True |   3,038.50 us | 4,683.52 us | 13,809.46 us |   1,513.450 us |   1,312.7000 us | 139,623.8 us |      100.0 |
|           LogWithProxyPattern | 34 |      True |     282.03 us |   717.33 us |  2,115.05 us |      54.550 us |      23.1000 us |  21,167.1 us |      100.0 |
| ProfileWithCastleDynamicProxy | 34 |      True |      29.33 us |    83.26 us |    245.50 us |       4.200 us |       2.0000 us |   2,459.1 us |      100.0 |
|   CacheWithCastleDynamicProxy | 34 |      True |   2,843.39 us | 4,398.34 us | 12,968.59 us |   1,522.500 us |   1,250.5000 us | 131,218.4 us |      100.0 |
|     LogWithCastleDynamicProxy | 34 |      True |     284.93 us |   722.08 us |  2,129.08 us |      56.900 us |      25.3000 us |  21,316.8 us |      100.0 |

---

## Benchmark of Different WCF Bindings

### Complete Result Set
|              Method |  n | optimized |         Mean |        Error |      StdDev |       Median |          Min |          Max | Iterations |
|-------------------- |--- |---------- |-------------:|-------------:|------------:|-------------:|-------------:|-------------:|-----------:|
|       WSHttpBinding | 34 |     False | 259,199.7 us | 13,615.19 us | 40,144.7 us | 243,625.0 us | 234,262.7 us | 456,658.3 us |      100.0 |
|       NetTcpBinding | 34 |     False | 263,610.6 us | 11,626.09 us | 34,279.8 us | 252,891.7 us | 236,485.4 us | 431,524.3 us |      100.0 |
| NetNamedPipeBinding | 34 |     False | 268,795.4 us | 11,064.98 us | 32,625.3 us | 261,013.1 us | 237,340.2 us | 414,609.1 us |      100.0 |
|       WSHttpBinding | 34 |      True |   1,950.1 us |    119.38 us |    352.0 us |   1,819.2 us |   1,618.0 us |   3,485.9 us |      100.0 |
|       NetTcpBinding | 34 |      True |     935.0 us |  1,303.51 us |  3,843.4 us |     407.4 us |     270.8 us |  38,758.2 us |      100.0 |
| NetNamedPipeBinding | 34 |      True |     604.4 us |    160.47 us |    473.2 us |     405.7 us |     240.4 us |   2,235.3 us |      100.0 |