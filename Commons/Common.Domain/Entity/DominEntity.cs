using Common.Domain.Event;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain.Entity;

public abstract class DominEntity: BaseEntity
{
    public DominEntity(Guid id)
    {
        Id = id;
    }
   
    /// <summary>
    /// 领域事件
    /// 如添加商品后通知第三方系统，或者其他操作
    /// </summary>
    [NotMapped] private List<IDomainEvent> domainEvents = new();

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