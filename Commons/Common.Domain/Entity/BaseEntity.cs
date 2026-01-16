namespace Common.Domain.Entity;

public abstract class BaseEntity : IEntity<Guid>
{
    protected BaseEntity()
    {
        IsDeleted = false;
        CreationTime = DateTime.Now;
    }

    public bool IsDeleted { get; private set; }

    public DateTime CreationTime { get; private set; }

    public string? LastModifierUser { get; private set; }

    public DateTime? LastModificationTime { get; private set; }

    public void SoftDelete()
    {
        IsDeleted = true;
    }

    public void SetLastModification(string user)
    {
        LastModifierUser = user ?? throw new ArgumentNullException(nameof(user));
        LastModificationTime = DateTime.Now;
    }
}