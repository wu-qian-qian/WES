namespace Common.Application.EventBus;

public interface IEventHandler<in TEvent, TResponse>
    where TEvent : IIntegrationEvent<TResponse>
{
    Task<TResponse> Handle(TEvent @event, CancellationToken cancellationToken = default);
}

public interface IEventHandler<in TEvent>
    where TEvent : IIntegrationEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken = default);
}