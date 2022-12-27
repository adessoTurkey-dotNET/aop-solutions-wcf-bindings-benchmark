using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace AOP.Services
{
    public static class RedisService
    {
        private static readonly IConfiguration _config = new ConfigurationBuilder().AddJsonFile("config.json")
                                                                                   .AddEnvironmentVariables()
                                                                                   .Build();

        private static readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { $"{_config.GetRequiredSection("REDIS").GetValue<string>("HOST")}:{_config.GetRequiredSection("REDIS").GetValue<string>("PORT")}" },
            Password = _config.GetRequiredSection("REDIS").GetValue<string>("PASS")
        });

        public static IDatabase db => _redis.GetDatabase();
    }

}
