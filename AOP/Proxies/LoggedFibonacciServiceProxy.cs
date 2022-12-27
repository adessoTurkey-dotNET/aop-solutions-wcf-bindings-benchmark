using AOP.Services;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace AOP.Proxies;

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
