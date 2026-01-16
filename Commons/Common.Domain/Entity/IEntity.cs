namespace Common.Domain.Entity;

public abstract class IEntity<T>
{
    public T Id { get; init; }
}