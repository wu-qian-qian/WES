using Common.Domain.Entity;

namespace Identity.Domain.Entities;

/// <summary>
///     中间表
///     用来连接权限和菜单的关联关系
/// </summary>
public class PermissionMenu : IEntity<Guid>
{
    public PermissionMenu()
    {
        Id = Guid.NewGuid();
    }

    public Guid PermissionId { get; set; }

    public Guid MenuId { get; set; }

    public Permission Permission { get; set; }
    public Menu Menu { get; set; }
}