using AOP.Models;
using AOP.Services;
using AOP.Services.Interfaces;
using Newtonsoft.Json;

namespace AOP.Proxies;

public class CachedWeatherServiceProxy : IWeatherService
{
    private readonly WeatherService _service;

    public CachedWeatherServiceProxy()
    {
        _service = new WeatherService();
    }

    public async Task<WeatherForecast?> RetrieveWeatherForecast(Location? cityLocation)
    {
        var cachedValue = await RedisService.db.StringGetAsync(cityLocation?.plateCode.ToString());
        if (cachedValue.IsNullOrEmpty)
        {
            var res = _service.RetrieveWeatherForecast(cityLocation);
            await RedisService.db.StringSetAsync(cityLocation?.plateCode.ToString(), JsonConvert.SerializeObject(res.Result));
            return res.Result;
        }

        return JsonConvert.DeserializeObject<WeatherForecast>(cachedValue!);
    }
}
