namespace Common.Domain;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}