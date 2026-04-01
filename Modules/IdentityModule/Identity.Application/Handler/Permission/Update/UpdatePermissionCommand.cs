using Common.Application.MediatR.Messaging;

namespace Identity.Application.Permission.Commands;

public class UpdatePermissionCommand : ICommand
{
    public Guid Id { get; set; }
    public string? PermissionCode { get; set; }
}
