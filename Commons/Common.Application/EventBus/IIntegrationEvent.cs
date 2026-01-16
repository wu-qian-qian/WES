namespace Common.Application.EventBus;

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