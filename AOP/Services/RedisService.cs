using StackExchange.Redis;

namespace AOP.Services
{
    public static class RedisService
    {
        private static readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { $"{Environment.GetEnvironmentVariable("REDIS_HOST")}:{Environment.GetEnvironmentVariable("REDIS_PORT")}" },
            Password = Environment.GetEnvironmentVariable("REDIS_PASSWORD")
        });

        public static IDatabase db => _redis.GetDatabase();
    }

}
