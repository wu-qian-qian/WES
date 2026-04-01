using Common.Application.MediatR.Messaging;

namespace Identity.Application.Permission.Commands;

public class DeletePermissionCommand : ICommand
{
    public Guid Id { get; set; }
}
