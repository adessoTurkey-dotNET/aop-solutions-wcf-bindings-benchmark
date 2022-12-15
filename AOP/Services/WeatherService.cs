using AOP.Models;
using AOP.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace AOP.Services;

public class WeatherService : IWeatherService
{
    private static readonly string _apiUrl = "https://api.open-meteo.com/v1";
    private static readonly string[] _dailyParameters = new[]
    {
            "weathercode",
            //"temperature_2m_max",
            //"temperature_2m_min",
            //"apparent_temperature_max",
            //"apparent_temperature_min",
            //"sunrise",
            //"sunset",
            //"precipitation_sum",
            //"rain_sum",
            //"showers_sum",
            //"snowfall_sum",
            //"precipitation_hours",
            //"windspeed_10m_max",
            //"windgusts_10m_max",
            //"winddirection_10m_dominant"
    };

    public async Task<WeatherForecast?> RetrieveWeatherForecast(Location? cityLocation)
    {
        if (cityLocation == null)
            return null;

        var sb = new StringBuilder();
        sb.Append($"{_apiUrl}/forecast?latitude={cityLocation.latitude}&longitude={cityLocation.longitude}&timezone=auto");
        sb.Append($"&daily={string.Join(",", _dailyParameters)}");

        var client = new HttpClient();
        var req = client.GetStreamAsync(sb.ToString());
        return await JsonSerializer.DeserializeAsync<WeatherForecast>(await req);
    }
}
