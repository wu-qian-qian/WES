using Common.Application.MediatR.Messaging;
using Common.Domain;

namespace Identity.Application.Role.Commands;

public class CreateRoleCommand : ICommand<Guid>
{
    public string RoleName { get; set; }
    public string? Description { get; set; }
}
