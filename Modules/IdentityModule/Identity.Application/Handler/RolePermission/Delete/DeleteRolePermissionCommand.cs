using Common.Application.MediatR.Messaging;

namespace Identity.Application.RolePermission.Commands;

public class DeleteRolePermissionCommand : ICommand
{
    public Guid Id { get; set; }
}
