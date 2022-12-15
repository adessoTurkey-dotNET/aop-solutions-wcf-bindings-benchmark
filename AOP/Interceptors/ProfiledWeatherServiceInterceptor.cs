using Castle.DynamicProxy;
using System.Diagnostics;

namespace AOP.Interceptors;

public class ProfiledWeatherServiceInterceptor : IInterceptor
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