using Common.Application.MediatR.Messaging;

namespace Identity.Application.RolePermission.Commands;

public class CreateRolePermissionCommand : ICommand<Guid>
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
}
