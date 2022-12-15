using AOP.Models;
using AOP.Services;
using AOP.Services.Interfaces;
using System.Diagnostics;

namespace AOP.Proxies;

public class ProfiledWeatherServiceProxy : IWeatherService
{
    private readonly WeatherService _service;

    public ProfiledWeatherServiceProxy()
    {
        _service = new WeatherService();
    }

    public Task<WeatherForecast?> RetrieveWeatherForecast(Location? cityLocation)
    {
        var watch = Stopwatch.StartNew();
        var res = _service.RetrieveWeatherForecast(cityLocation);
        Console.WriteLine($"Service time: {watch.ElapsedMilliseconds}ms");
        return res;
    }
}
