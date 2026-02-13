using Common.Application.Caching;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Common.Infrastructure.Caching;
public static class CacheConfiguration
{
    public static IServiceCollection AddCacheConfiguration(this IServiceCollection serviceDescriptors
    ,string redisConnectionString = "")
    {
     try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            serviceDescriptors.AddSingleton(connectionMultiplexer);
            serviceDescriptors.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
        }
        catch
        {
            serviceDescriptors.AddDistributedMemoryCache();
        }

        serviceDescriptors.AddSingleton<ICacheService, CacheService>();
        return serviceDescriptors;   
    }
}