using System.Collections.Concurrent;

namespace Common.Infrastructure.EventBus.Manager;

/// <summary>
///     无返回的事件总线管理中心
/// </summary>
internal sealed partial class EventManager
{
    private static readonly ConcurrentDictionary<string, Type> _handlers = new();

    public bool HasSubscriptionsForEvent(string eventName)
    {
        return _handlers.ContainsKey(eventName);
    }

    public Type GetHandlerForEvent(string eventName)
    {
        return _handlers[eventName];
    }

    /// <summary>
    ///     添加订阅
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="handler"></param>
    public void AddSubscription(string eventName, Type handlers)
    {
        if (!HasSubscriptionsForEvent(eventName)) _handlers.TryAdd(eventName, handlers);
        else
            throw new AggregateException(eventName + " 已经存在订阅");
    }
}