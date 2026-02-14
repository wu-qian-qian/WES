using Common.Application.DecoratorEvent;
using Common.Domain.Event;

namespace Common.Infrastructure.DecoratorEvent;

/// <summary>
///     相当于一个装饰器
///     可以做一些其他处理
///    例如：幂等性检查、日志记录、异常处理等
/// </summary>
/// <param name="decorated">真正的业务实现</param>
/// <typeparam name="TDomainEvent"></typeparam>
public sealed class IdempotentDomainEventHandler<TDomainEvent>(
    IDomainEventHandler<TDomainEvent> decorated)
    : DomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public override async Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // 可以在这里添加幂等性检查逻辑 
        await decorated.Handle(domainEvent, cancellationToken);
    }
}