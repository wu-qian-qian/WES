using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

public abstract class IEntity
{
    public IEntity(Guid id)
    {
        Id = id;
        IsDeleted = false;
        CreationTime = DateTime.Now;
    }

    public Guid Id { get; init; }

    public DateTime CreationTime { get; private set; }

    public string? LastModifierUser { get; private set; }

    public DateTime? LastModificationTime { get; private set; }

    public void SetLastModification(string user)
    {
        LastModifierUser = user ?? throw new ArgumentNullException(nameof(user));
        LastModificationTime = DateTime.Now;
    }

    public bool IsDeleted { get; private set; }

    public void SoftDelete()
    {
        IsDeleted = true;
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