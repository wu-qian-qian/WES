using Common.Application.MediatR.Messaging;

namespace Identity.Application.PermissionMenu.Commands;

public class UpdatePermissionMenuCommand : ICommand
{
    public Guid Id { get; set; }
    public Guid PermissionId { get; set; }
    public Guid MenuId { get; set; }
}
