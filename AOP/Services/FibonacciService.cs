namespace AOP.Services;

public class FibonacciService : IFibonacciService
{
    private readonly Dictionary<int, ulong> _cache = new Dictionary<int, ulong>();

    private ulong CalculateWithoutCache(int n)
    {
        n = n < 0 ? 0 : n;

        if (n < 2)
            return (ulong)n;

        _cache[n] = CalculateWithoutCache(n - 1) + CalculateWithoutCache(n - 2);
        return _cache[n];
    }

    private ulong CalculateWithCache(int n)
    {
        n = n < 0 ? 0 : n;

        if (n < 2)
            return (ulong)n;

        if (_cache.ContainsKey(n))
        {
            return _cache[n];
        }

        _cache[n] = CalculateWithCache(n - 1) + CalculateWithCache(n - 2);
        return _cache[n];
    }

    public ulong Calculate(int n, bool optimized)
    {
        if (optimized)
        {
            return CalculateWithCache(n);
        }

        return CalculateWithoutCache(n);
    }
}
