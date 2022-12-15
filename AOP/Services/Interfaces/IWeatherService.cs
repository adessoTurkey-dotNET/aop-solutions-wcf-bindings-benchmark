using AOP.Models;

namespace AOP.Services.Interfaces;

public interface IWeatherService
{
    Task<WeatherForecast?> RetrieveWeatherForecast(Location? cityLocation);
}
