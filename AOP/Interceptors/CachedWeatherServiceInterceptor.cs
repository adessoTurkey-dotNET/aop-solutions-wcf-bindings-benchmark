using Castle.DynamicProxy;

namespace AOP.Interceptors;

public class CachedWeatherServiceInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine($"Before target call {invocation.Method.Name}");
        try
        {
            invocation.Proceed();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Target exception {e.Message}");
            throw;
        }
        finally
        {
            Console.WriteLine($"After target call {invocation.Method.Name}");
        }
    }
}
