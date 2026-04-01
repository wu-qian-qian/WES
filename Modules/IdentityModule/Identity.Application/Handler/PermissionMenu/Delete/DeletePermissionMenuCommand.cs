using Common.Application.MediatR.Messaging;

namespace Identity.Application.PermissionMenu.Commands;

public class DeletePermissionMenuCommand : ICommand
{
    public Guid Id { get; set; }
}
