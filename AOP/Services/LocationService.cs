using AOP.Services.Interfaces;

namespace AOP.Services;

public record Location(int plateCode, double latitude, double longitude);

public class LocationService : ILocationService
{
    private readonly Dictionary<string, Location> _data = new()
    {
        { "Adana", new Location(01, 36.9975315, 35.2180278) },
        { "Ankara", new Location(06, 39.9032861, 32.4757038) },
        { "Antalya", new Location(07, 36.8980011, 30.6830858) },
        { "Bursa", new Location(16, 40.2217383, 28.9972702) },
        { "İstanbul", new Location(34, 41.0054958, 28.8720984) },
        { "İzmir", new Location(35, 38.417722, 26.9361971) },
    };

    public Location? Get(string city) => _data.ContainsKey(city) ? _data[city] : null;
}
