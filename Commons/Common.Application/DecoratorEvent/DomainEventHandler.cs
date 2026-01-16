using Common.Domain.Event;

namespace Common.Application.DecoratorEvent;

/// <summary>
///     继承使用 类需要为public
/// </summary>
/// <typeparam name="TDomainEvent"></typeparam>
public abstract class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public abstract Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);

    public Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return Handle((TDomainEvent)domainEvent, cancellationToken);
    }
}