using Common.Application.DecoratorEvent;
using Common.Domain.Event;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.DecoratorEvent;

public class MediatrDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public MediatrDomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch<T>(T domainEvent, CancellationToken ct = default) where T : IDomainEvent
    {
        // 从 DI 容器获取所有处理 T 的处理器
        var handlers = _serviceProvider.GetServices<IDomainEventHandler<T>>();
        foreach (var handler in handlers) await handler.Handle(domainEvent, ct);
    }
}