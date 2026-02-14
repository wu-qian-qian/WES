using Common.Application.EventBus;
using Common.Infrastructure.EventBus.Manager;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.EventBus.Bus;

internal sealed class IntegrationEventBus : IIntegrationEventBus
{
    private readonly EventManager _eventManager;
    private readonly IServiceProvider _serviceProvider;

    public IntegrationEventBus(EventManager eventManager, IServiceScopeFactory serviceScopeFactory)
    {
        _eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
        _serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
    }

    [Obsolete("内部使用dynamic不推荐使用")]
    public async Task<TResponse> PublishAsync2<TResponse>(IIntegrationEvent<TResponse> integrationEvent,
        CancellationToken cancellationToken = default)
    {
        var eventName = integrationEvent.GetType().FullName ??
                        throw new ArgumentNullException(nameof(integrationEvent), "Event type name cannot be null.");
        if (_eventManager.HasSubscriptionsForEvent(eventName))
        {
            var handels = _eventManager.GetHandlerForEvent(eventName);
            var handler = _serviceProvider.GetService(handels);
            try
            {
                dynamic dynHandler = handler;
                dynamic dynEvent = integrationEvent;
                return await dynHandler.Handle(dynEvent, cancellationToken);
            }
            catch (RuntimeBinderException ex)
            {
            }
        }

        throw new ArgumentNullException(nameof(integrationEvent), "Event type name cannot be null.");
    }

    public async Task<TResponse> PublishAsync<TResponse>(IIntegrationEvent<TResponse> @event,
        CancellationToken ct = default)
    {
        var eventType = @event.GetType();

        if (!_eventManager.TryGetHandler(eventType.FullName, out var handlerType))
            throw new InvalidOperationException($"No handlers registered for event: {eventType}");

        TResponse result = default!;
        var hasResult = false;

        var handlerDelegate =
            _eventManager.GetOrAddHandlerDelegate<TResponse>(eventType, handlerType, typeof(TResponse));

        // 2. 从 DI 解析处理器
        var handler = _serviceProvider.GetRequiredService(handlerType);

        // 3. 直接调用（无反射！）
        var task = handlerDelegate(handler, @event, ct);
        var response = await task.ConfigureAwait(false);

        // 假设只取第一个结果（或合并逻辑）
        if (!hasResult)
        {
            result = response;
            hasResult = true;
        }

        if (!hasResult)
            throw new InvalidOperationException("Handler returned no result.");

        return result;
    }

    /// <summary>
    ///     发布事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="integrationEvent"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent
    {
        var eventName = integrationEvent.GetType().FullName ??
                        throw new ArgumentNullException(nameof(integrationEvent), "Event type name cannot be null.");
        if (_eventManager.HasSubscriptionsForEvent(eventName))
        {
            var handel = _eventManager.GetHandlerForEvent(eventName);
            var scop = _serviceProvider.CreateScope();
            var handler = scop.ServiceProvider.GetService(handel);
            if (handler is IIntegrationEventHandler<T> eventHandler)
                await eventHandler.Handle(integrationEvent, cancellationToken);
        }
    }
}