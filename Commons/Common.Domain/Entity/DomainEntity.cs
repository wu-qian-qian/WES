using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain.Event;

namespace Common.Domain.Entity;

public abstract class DomainEntity : BaseEntity
{
    /// <summary>
    ///     领域事件
    ///     如添加商品后通知第三方系统，或者其他操作
    /// </summary>
    [NotMapped] private readonly List<IDomainEvent> domainEvents = new();

    public DomainEntity(Guid id)
    {
        Id = id;
    }

    public IEnumerable<IDomainEvent> GetDomainEvents()
    {
        return domainEvents;
    }

    public void AddDomainEvent(IDomainEvent eventItem)
    {
        domainEvents.Add(eventItem);
    }

    public void ClearDomainEvent()
    {
        domainEvents.Clear();
    }
}