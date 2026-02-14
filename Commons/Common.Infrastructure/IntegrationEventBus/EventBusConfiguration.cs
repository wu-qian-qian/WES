using System.Reflection;
using Common.Application.EventBus;
using Common.Infrastructure.EventBus.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.EventBus;

public static class EventBusConfiguration
{
    public static IServiceCollection AddEventBusConfiguration(this IServiceCollection serviceCollection,
        Assembly[] assArray)
    {
        var eventManager = GetEventTypes(assArray, serviceCollection);
        serviceCollection.AddSingleton<IIntegrationEventBus>(sp =>
        {
            var serviceScope = sp.GetRequiredService<IServiceScopeFactory>();
            return new Bus.IntegrationEventBus(eventManager, serviceScope);
        });
        return serviceCollection;
    }

    /// <summary>
    ///     获取事件类型和对应的处理器类型
    /// </summary>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static EventManager GetEventTypes(Assembly[] assemblies, IServiceCollection serviceCollection)
    {
        if (assemblies == null || assemblies.Length == 0)
            throw new ArgumentException("At least one assembly must be provided.", nameof(assemblies));
        //程序集的所有类
        var assTypeList = assemblies.Select(p => p.GetTypes());
        var allTypeList = new List<Type>();
        foreach (var item in assTypeList) allTypeList.AddRange(item);
        //筛选出所有的事件类型

        #region 开启型泛型注入

        var handlerTypes = allTypeList.Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType &&
                                                   i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<,>))).ToArray();

        // 构建事件类型 -> 处理器类型列表 的映射
        var eventToHandlers = new Dictionary<string, Type>();

        foreach (var handlerType in handlerTypes)
        {
            var interfaces = handlerType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<,>));

            if (!interfaces.Any() || interfaces.Count() > 1)
                throw new ArgumentException($"{handlerType.FullName} 必须实现且只能实现一个 IEventHandler<TEvent, TResponse> 接口");

            // 获取事件类型 IEventHandler<,> 第一个泛型参数 IEvent 第二个返回类型 TResponse
            var eventType = interfaces.First().GetGenericArguments()[0];
            if (!eventToHandlers.ContainsKey(eventType.FullName))
            {
                eventToHandlers[eventType.FullName] = handlerType;
                serviceCollection.AddTransient(handlerType);
            }
        }

        #endregion

        var eventManager = new EventManager(eventToHandlers);

        #region 普通事件注入

        var handlerDomains = allTypeList.Where(type => type is { IsAbstract: false, IsInterface: false } &&
                                                       type.IsAssignableTo(typeof(IIntegrationEvent)));
        foreach (var item in handlerDomains)
        {
            //IEventHandler<item>接口的处理器类型
            var handlers = allTypeList
                .Where(t => t is { IsAbstract: false, IsInterface: false } &&
                            t.IsAssignableTo(typeof(IIntegrationEventHandler<>).MakeGenericType(item)))
                .ToArray();
            if (handlers.Length == 0 || handlers.Length > 1) continue;

            serviceCollection.AddTransient(handlers[0]);
            eventManager.AddSubscription(item.FullName, handlers[0]);
        }

        #endregion

        return eventManager;
    }
}