using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace AOP.Interceptors;

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