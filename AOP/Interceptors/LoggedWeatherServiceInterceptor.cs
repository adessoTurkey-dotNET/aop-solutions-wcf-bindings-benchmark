using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace AOP.Interceptors;

public class LoggedWeatherServiceInterceptor : IInterceptor
{
    private readonly ILogger _logger;

    public LoggedWeatherServiceInterceptor()
    {
        _logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<LoggedWeatherServiceInterceptor>();
    }

    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine($"Before target call {invocation.Method.Name}");
        try
        {
            invocation.Proceed();
            _logger.LogTrace(invocation.ReturnValue.ToString());
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