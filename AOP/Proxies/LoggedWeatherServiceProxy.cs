using AOP.Models;
using AOP.Services;
using AOP.Services.Interfaces;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace AOP.Proxies;

public class LoggedWeatherServiceProxy : IWeatherService
{
    private readonly WeatherService _service;
    private readonly ILogger _logger;

    public LoggedWeatherServiceProxy()
    {
        _service = new WeatherService();
        _logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<LoggedWeatherServiceProxy>();
    }

    public Task<WeatherForecast?> RetrieveWeatherForecast(Location? cityLocation)
    {
        var res = _service.RetrieveWeatherForecast(cityLocation);
        _logger.LogTrace(res.ToString());
        return res;
    }
}
