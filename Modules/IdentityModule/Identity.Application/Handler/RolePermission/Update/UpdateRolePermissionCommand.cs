using Common.Application.MediatR.Messaging;

namespace Identity.Application.RolePermission.Commands;

public class UpdateRolePermissionCommand : ICommand
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
}
