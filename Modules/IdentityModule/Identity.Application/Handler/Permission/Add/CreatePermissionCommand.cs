using Common.Application.MediatR.Messaging;

namespace Identity.Application.Permission.Commands;

public class CreatePermissionCommand : ICommand<Guid>
{
    public string PermissionCode { get; set; } = string.Empty;
}
