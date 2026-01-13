using Common.Application.Caching;
using Common.Infrastructure.Caching;
using Common.Infrastructure.DecoratorEvent;
using Common.Infrastructure.DependencyInjection;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.FSM;
using Common.Infrastructure.Log;
using Common.Infrastructure.MediatR;
using Common.Infrastructure.Net;
using Common.Infrastructure.Net.Http;
using Common.Infrastructure.Quartz;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;

namespace Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection serviceCollection,
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
/// MediatR 中介者注入
/// </summary>
/// <param name="services"></param>
/// <param name="moduleAssemblies"></param>
/// <param name="behaviors"></param>
/// <returns></returns>
    public static IServiceCollection AddMediatRConfiguration(IServiceCollection services,
        Assembly[] moduleAssemblies, Action<MediatRServiceConfiguration>[] behaviors)
    {
        return services.AddMediatRConfiguration(moduleAssemblies, behaviors);
    }

/// <summary>
///  API 请求封装
/// </summary>
/// <param name="serviceCollection"></param>
/// <param name="options"></param>
/// <returns></returns>
    public static IServiceCollection AddHttpConfiguration(IServiceCollection serviceCollection,
        HttpOptions? options = null)
    {
        return serviceCollection.AddHttpConfiguration(options);
    }
/// <summary>
/// Redis注入
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