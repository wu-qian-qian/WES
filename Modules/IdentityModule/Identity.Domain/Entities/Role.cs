using Common.Domain.Entity;

namespace Identity.Domain.Entities;

public class Role : BaseEntity
{
    public Role()
    {
        Id = Guid.NewGuid();
    }

    public string RoleName { get; set; }
    public string? Description { get; set; }

    // 关联用户
    public ICollection<UserRole> UserRoles { get; set; }

    // 关联权限
    public ICollection<RolePermission> RolePermissions { get; set; }
}