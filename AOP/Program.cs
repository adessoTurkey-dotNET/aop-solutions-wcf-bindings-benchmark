using AOP.Interceptors;
using AOP.Proxies;
using AOP.Services;
using AOP.Services.Interfaces;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using Castle.DynamicProxy;

[SimpleJob(RunStrategy.Monitoring, targetCount: 5)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn, IterationsColumn]
public class Program
{
    private static readonly IFibonacciService _fibonacciService = new FibonacciService();
    private static readonly ILocationService _locationService = new LocationService();
    private static readonly IWeatherService _weatherService = new WeatherService();

    private static readonly IWeatherService _profiledWeatherServiceProxy = new ProfiledWeatherServiceProxy();
    private static readonly IWeatherService _cachedWeatherServiceProxy = new CachedWeatherServiceProxy();
    private static readonly IWeatherService _loggedWeatherServiceProxy = new LoggedWeatherServiceProxy();

    private static readonly ProxyGenerator _proxyGenerator = new();
    private static readonly IWeatherService _weatherServiceProfilingInterceptor = (IWeatherService)_proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IWeatherService), new WeatherService(), new ProfiledWeatherServiceInterceptor());
    private static readonly IWeatherService _weatherServiceCachingInterceptor = (IWeatherService)_proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IWeatherService), new WeatherService(), new CachedWeatherServiceInterceptor());
    private static readonly IWeatherService _weatherServiceLoggingInterceptor = (IWeatherService)_proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IWeatherService), new WeatherService(), new LoggedWeatherServiceInterceptor());

    [Params("Bursa", "Ankara", "İzmir")]
    public static string _city;

    public static async Task Main(string[] args)
    {
        BenchmarkRunner.Run<Program>();
    }

    [Benchmark]
    public async Task<object?> Default()
    {
        var cityLocation = _locationService.Get(_city);
        var data = await _weatherService.RetrieveWeatherForecast(cityLocation);
        if (cityLocation == null || data == null)
            return null;

        var fib = _fibonacciService.CalculateWithCache(cityLocation.plateCode);
        //return fib;
        return new
        {
            PlateCode = cityLocation.plateCode,
            FibonacciOfPlateCode = fib,
            WeatherForecast = data
        };
    }

    #region PROXY PATTERN
    [Benchmark]
    public async Task<object?> ProfileWithProxyPattern()
    {
        var cityLocation = _locationService.Get(_city);
        var data = await _profiledWeatherServiceProxy.RetrieveWeatherForecast(cityLocation);
        if (cityLocation == null || data == null)
            return null;

        var fib = _fibonacciService.CalculateWithCache(cityLocation.plateCode);
        //return fib;
        return new
        {
            PlateCode = cityLocation.plateCode,
            FibonacciOfPlateCode = fib,
            WeatherForecast = data
        };
    }

    [Benchmark]
    public async Task<object?> CacheWithProxyPattern()
    {
        var cityLocation = _locationService.Get(_city);
        var data = await _cachedWeatherServiceProxy.RetrieveWeatherForecast(cityLocation);
        if (cityLocation == null || data == null)
            return null;

        var fib = _fibonacciService.CalculateWithCache(cityLocation.plateCode);
        //return fib;
        return new
        {
            PlateCode = cityLocation.plateCode,
            FibonacciOfPlateCode = fib,
            WeatherForecast = data
        };
    }

    [Benchmark]
    public async Task<object?> LogWithProxyPattern()
    {
        var cityLocation = _locationService.Get(_city);
        var data = await _loggedWeatherServiceProxy.RetrieveWeatherForecast(cityLocation);
        if (cityLocation == null || data == null)
            return null;

        var fib = _fibonacciService.CalculateWithCache(cityLocation.plateCode);
        //return fib;
        return new
        {
            PlateCode = cityLocation.plateCode,
            FibonacciOfPlateCode = fib,
            WeatherForecast = data
        };
    }
    #endregion

    #region CASTLE DYNAMICPROXY
    [Benchmark]
    public async Task<object?> ProfileWithCastleDynamicProxy()
    {
        var cityLocation = _locationService.Get(_city);
        var data = await _weatherServiceProfilingInterceptor.RetrieveWeatherForecast(cityLocation);
        if (cityLocation == null || data == null)
            return null;

        var fib = _fibonacciService.CalculateWithCache(cityLocation.plateCode);
        //return fib;
        return new
        {
            PlateCode = cityLocation.plateCode,
            FibonacciOfPlateCode = fib,
            WeatherForecast = data
        };
    }

    [Benchmark]
    public async Task<object?> CacheWithCastleDynamicProxy()
    {
        var cityLocation = _locationService.Get(_city);
        var data = await _weatherServiceCachingInterceptor.RetrieveWeatherForecast(cityLocation);
        if (cityLocation == null || data == null)
            return null;

        var fib = _fibonacciService.CalculateWithCache(cityLocation.plateCode);
        //return fib;
        return new
        {
            PlateCode = cityLocation.plateCode,
            FibonacciOfPlateCode = fib,
            WeatherForecast = data
        };
    }

    [Benchmark]
    public async Task<object?> LogWithCastleDynamicProxy()
    {
        var cityLocation = _locationService.Get(_city);
        var data = await _weatherServiceLoggingInterceptor.RetrieveWeatherForecast(cityLocation);
        if (cityLocation == null || data == null)
            return null;

        var fib = _fibonacciService.CalculateWithCache(cityLocation.plateCode);
        //return fib;
        return new
        {
            PlateCode = cityLocation.plateCode,
            FibonacciOfPlateCode = fib,
            WeatherForecast = data
        };
    }
    #endregion
}

