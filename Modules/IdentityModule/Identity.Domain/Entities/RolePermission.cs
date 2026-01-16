using Common.Domain.Entity;

namespace Identity.Domain.Entities;

public class RolePermission : IEntity<Guid>
{
    public RolePermission()
    {
        Id = Guid.NewGuid();
    }

    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public Role Role { get; set; }
    public Permission Permission { get; set; }
}