namespace Common.Application.EventBus;

/// <summary>
///    事件处理器接口
/// </summary>
/// <typeparam name="TEvent"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IIntegrationEventHandler<in TEvent, TResponse>
    where TEvent : IIntegrationEvent<TResponse>
{
    Task<TResponse> Handle(TEvent @event, CancellationToken cancellationToken = default);
}

/// <summary>
///   无返回值事件处理器接口
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public interface IIntegrationEventHandler<in TEvent>
    where TEvent : IIntegrationEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken = default);
}