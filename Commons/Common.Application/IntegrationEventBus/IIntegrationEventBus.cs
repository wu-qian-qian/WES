namespace Common.Application.EventBus;

/// <summary>
///    集成 事件总线接口
/// </summary>
public interface IIntegrationEventBus
{
    Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent;

    [Obsolete("内部使用dynamic不推荐使用")]
    Task<TResponse> PublishAsync2<TResponse>(IIntegrationEvent<TResponse> integrationEvent,
        CancellationToken cancellationToken = default);

    Task<TResponse> PublishAsync<TResponse>(
        IIntegrationEvent<TResponse> @event,
        CancellationToken ct = default);
}