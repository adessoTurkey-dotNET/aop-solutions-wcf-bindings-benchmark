using AOP.Services;

namespace AOP.Proxies;

public class CachedFibonacciServiceProxy : IFibonacciService
{
    private readonly FibonacciService _service;

    public CachedFibonacciServiceProxy()
    {
        _service = new FibonacciService();
    }

    public ulong Calculate(int n, bool optimized)
    {
        var cachedValue = RedisService.db.StringGet(n.ToString());
        if (cachedValue.IsNullOrEmpty)
        {
            var res = _service.Calculate(n, optimized);
            RedisService.db.StringSet(n.ToString(), res.ToString());
            return res;
        }

        return Convert.ToUInt64(cachedValue);
    }
}
