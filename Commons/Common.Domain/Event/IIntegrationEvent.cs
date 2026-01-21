namespace Common.Application.EventBus;

//事件总线事件


/// <summary>
///     无返回值集成事件
/// </summary>
public interface IIntegrationEvent
{
}

/// <summary>
///     有返回值集成事件
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IIntegrationEvent<out TResponse>
{
}