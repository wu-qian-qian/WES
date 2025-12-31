namespace Common.Domain.Event;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}