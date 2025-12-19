using Common.Domain;

namespace Common.Application.DecoratorEvent;

public interface IDomainEventDispatcher
{
    Task Dispatch<T>(T domainEvent, CancellationToken ct = default) where T : IDomainEvent;
}