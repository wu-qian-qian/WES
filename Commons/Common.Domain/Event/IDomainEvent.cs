namespace Common.Domain.Event;

/// <summary>
/// 装饰器 事件总线
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    ///     事件唯一标识
    /// </summary>
    Guid EventId { get; }

    DateTime OccurredOnUtc { get; }
}