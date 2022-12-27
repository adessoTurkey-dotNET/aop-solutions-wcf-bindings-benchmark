using AOP.Interceptors;
using AOP.Proxies;
using AOP.Services;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using Castle.DynamicProxy;

[SimpleJob(RunStrategy.Monitoring, iterationCount: 100)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn, IterationsColumn]
public class Program
{
    private static readonly IFibonacciService _fibonacciService = new FibonacciService();
    private static readonly IFibonacciService _profiledFibonacciServiceProxy = new ProfiledFibonacciServiceProxy();
    private static readonly IFibonacciService _cachedFibonacciServiceProxy = new CachedFibonacciServiceProxy();
    private static readonly IFibonacciService _loggedFibonacciServiceProxy = new LoggedFibonacciServiceProxy();

    private static readonly ProxyGenerator _proxyGenerator = new();
    private static readonly IFibonacciService _fibonacciServiceProfilingInterceptor = (IFibonacciService)_proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IFibonacciService), new FibonacciService(), new ProfiledFibonacciServiceInterceptor());
    private static readonly IFibonacciService _fibonacciServiceCachingInterceptor = (IFibonacciService)_proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IFibonacciService), new FibonacciService(), new CachedFibonacciServiceInterceptor());
    private static readonly IFibonacciService _fibonacciServiceLoggingInterceptor = (IFibonacciService)_proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IFibonacciService), new FibonacciService(), new LoggedFibonacciServiceInterceptor());

    [Params(34)]
    public static int n;

    [Params(true, false)]
    public static bool optimized;

    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<Program>();
    }

    [Benchmark]
    public ulong Default()
    {
        return _fibonacciService.Calculate(n, optimized);
    }

    #region PROXY PATTERN
    [Benchmark]
    public ulong ProfileWithProxyPattern()
    {
        return _profiledFibonacciServiceProxy.Calculate(n, optimized);
    }

    [Benchmark]
    public ulong CacheWithProxyPattern()
    {
        return _cachedFibonacciServiceProxy.Calculate(n, optimized);
    }

    [Benchmark]
    public ulong LogWithProxyPattern()
    {
        return _loggedFibonacciServiceProxy.Calculate(n, optimized);
    }
    #endregion

    #region CASTLE DYNAMICPROXY
    [Benchmark]
    public ulong ProfileWithCastleDynamicProxy()
    {
        return _fibonacciServiceProfilingInterceptor.Calculate(n, optimized);
    }

    [Benchmark]
    public ulong CacheWithCastleDynamicProxy()
    {
        return _fibonacciServiceCachingInterceptor.Calculate(n, optimized);
    }

    [Benchmark]
    public ulong LogWithCastleDynamicProxy()
    {
        return _fibonacciServiceLoggingInterceptor.Calculate(n, optimized);
    }
    #endregion
}

