using Common.Application.Caching;
using Common.Infrastructure.Caching;
using Common.Infrastructure.DependencyInjection;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.FSM;
using Common.Infrastructure.Log;
using Common.Infrastructure.MediatR;
using Common.Infrastructure.Net;
using Common.Infrastructure.Net.Http;
using Common.Infrastructure.Quartz;
using Common.JsonExtension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;
using System.Reflection;
using Common.Infrastructure.DecoratorEvent;

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

    public static IServiceCollection AddMediarConfiguration(IServiceCollection services,
        Assembly[] moduleAssemblies, Action<MediatRServiceConfiguration>[] behaviors)
    {
        return services.AddMediatRConfiguration(moduleAssemblies, behaviors);
    }

    public static IServiceCollection AddHttpConfiguration(IServiceCollection serviceCollection,
        HttpOptions options = null)
    {
        return serviceCollection.AddHttpConfiguration(options);
    }

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