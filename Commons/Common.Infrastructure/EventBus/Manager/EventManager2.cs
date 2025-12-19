using System.Collections.Concurrent;
using System.Linq.Expressions;
using Common.Application.EventBus;

namespace Common.Infrastructure.EventBus.Manager;

/// <summary>
/// 一些需要触发返回值的本地事件总线
/// </summary>
internal sealed partial class EventManager
{
    // 注册时由 DI 注入所有处理器类型
    private readonly IReadOnlyDictionary<string, Type> _eventToHandlerTypes;

    private readonly ConcurrentDictionary<string, Delegate> _handlerDelegates = new();

    public EventManager(IReadOnlyDictionary<string, Type> eventToHandlerTypes)
    {
        _eventToHandlerTypes = eventToHandlerTypes;
    }

    public bool TryGetHandler(string eventName, out Type handler)
    {
        _eventToHandlerTypes.TryGetValue(eventName, out handler);
        if (handler != null)
            return true;
        return false;
    }

    public Func<object, object, CancellationToken, Task<TResponse>> GetOrAddHandlerDelegate<TResponse>(
        Type eventType,
        Type handlerType,
        Type responseType)
    {
        // 缓存键：(handlerType, eventType)
        var key = (handlerType, eventType);

        // 注意：这里需要为每个 TResponse 生成不同委托，所以用 responseType 参与 key
        // 但我们可以用非泛型委托 + object，再 cast —— 更简单的方式是下面的工厂

        // 更优：使用泛型工厂方法（见下方）
        var stringKey = $"{handlerType.FullName}-{eventType.FullName}";
        //如果存在直接返回 反之则创建
        return (Func<object, object, CancellationToken, Task<TResponse>>)
            _handlerDelegates.GetOrAdd(stringKey, _ =>
            {
                // 构造接口类型：IEventHandler<eventType, responseType>
                var interfaceType = typeof(IEventHandler<,>)
                    .MakeGenericType(eventType, responseType);

                if (!interfaceType.IsAssignableFrom(handlerType))
                    throw new InvalidOperationException($"Handler {handlerType} does not implement {interfaceType}");

                var handleMethod = interfaceType.GetMethod("Handle")!;

                // 创建强类型委托
                var handlerParam = Expression.Parameter(typeof(object), "handler");
                var eventParam = Expression.Parameter(typeof(object), "event");
                var ctParam = Expression.Parameter(typeof(CancellationToken), "ct");

                var call = Expression.Call(
                    Expression.Convert(handlerParam, handlerType),
                    handleMethod,
                    Expression.Convert(eventParam, eventType),
                    ctParam
                );

                var lambda = Expression.Lambda<Func<object, object, CancellationToken, Task<TResponse>>>(
                    call, handlerParam, eventParam, ctParam);

                return lambda.Compile();
            });
    }
}