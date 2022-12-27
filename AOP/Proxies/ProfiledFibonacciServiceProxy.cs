using AOP.Services;
using System.Diagnostics;

namespace AOP.Proxies;

public class ProfiledFibonacciServiceProxy : IFibonacciService
{
    private readonly FibonacciService _service;

    public ProfiledFibonacciServiceProxy()
    {
        _service = new FibonacciService();
    }

    public ulong Calculate(int n, bool optimized)
    {
        var watch = Stopwatch.StartNew();
        var res = _service.Calculate(n, optimized);
        Console.WriteLine($"Service time: {watch.ElapsedMilliseconds}ms");
        return res;
    }
}
