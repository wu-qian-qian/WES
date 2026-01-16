using System.Reflection;
using Common.Application.Caching;
using Common.Infrastructure.Caching;
using Common.Infrastructure.DecoratorEvent;
using Common.Infrastructure.DependencyInjection;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.FSM;
using Common.Infrastructure.Log;
using Common.Infrastructure.Quartz;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Common.Infrastructure;

public static class InfranstructureConfiguration
{
    public static IServiceCollection AddInfranstructureConfiguration(this IServiceCollection serviceCollection,
        params Assembly[] assArray)
    {
        //带有装饰器 的事件总线
        serviceCollection.AddDecoratorConfiguration(assArray);
        //依赖特性注入
        serviceCollection.AddDependyConfiguration(assArray);
        //事件总线注入
        serviceCollection.AddEventBusConfiguration(assArray);
        //状态机注入
        serviceCollection.AddFsmConfiguration(assArray);
        //日志配置
        serviceCollection.AddSeriLogConfiguration();
        //Job注入
        serviceCollection.AddQuatrzJobConfiguration();
        return serviceCollection;
    }

    /// <summary>
    ///     Redis注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="redisConnectionString"></param>
    /// <returns></returns>
    public static IServiceCollection AddRedisCacheing(IServiceCollection services, string redisConnectionString = "")
    {
        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.AddSingleton(connectionMultiplexer);
            services.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.AddSingleton<ICacheService, CacheService>();
        return services;
    }
}