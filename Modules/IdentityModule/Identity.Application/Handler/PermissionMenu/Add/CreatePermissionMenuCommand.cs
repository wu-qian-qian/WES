using Common.Application.MediatR.Messaging;

namespace Identity.Application.PermissionMenu.Commands;

public class CreatePermissionMenuCommand : ICommand<Guid>
{
    public Guid PermissionId { get; set; }
    public Guid MenuId { get; set; }
}
