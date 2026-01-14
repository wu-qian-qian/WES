namespace Common.Domain.Event;

public interface IDomainEvent
{
    /// <summary>
    /// 事件唯一标识
    /// </summary>
    Guid EventId { get; }
    DateTime OccurredOnUtc { get; }
}