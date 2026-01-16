using Common.Domain.Entity;

namespace Identity.Domain.Entities;

/// <summary>
///     权限表
/// </summary>
public class Permission : IEntity<Guid>
{
    public Permission()
    {
        Id = Guid.NewGuid();
    }

    public string PermissionCode { get; set; }

    public ICollection<PermissionMenu> PermissionMenus { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }
}