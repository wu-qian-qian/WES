using Common.Application.DecoratorEvent;
using Common.Domain;

namespace Common.Infrastructure.DecoratorEvent;

/// <summary>
/// 相当于一个装饰器
/// 可以做一些其他处理
/// </summary>
/// <param name="decorated">真正的业务实现</param>
/// <typeparam name="TDomainEvent"></typeparam>
internal sealed class IdempotentDomainEventHandler<TDomainEvent>(
    IDomainEventHandler<TDomainEvent> decorated)
    : DomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public override async Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await decorated.Handle(domainEvent, cancellationToken);
    }
}