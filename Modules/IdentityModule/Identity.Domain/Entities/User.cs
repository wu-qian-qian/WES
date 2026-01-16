using Common.Domain.Entity;

namespace Identity.Domain.Entities;

public class User : DomainEntity
{
    public User() : base(Guid.NewGuid())
    {
        IsEnabled = true;
    }

    public string Username { get; set; }
    public string PasswordHash { get; set; }

    public string Nickname { get; set; }

    public bool IsEnabled { get; set; }

    // 关联角色（多对多）
    public ICollection<UserRole> UserRoles { get; set; }
}