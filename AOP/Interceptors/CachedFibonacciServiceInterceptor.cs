using AOP.Services;
using Castle.DynamicProxy;

namespace AOP.Interceptors;

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
