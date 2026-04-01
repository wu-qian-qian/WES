using Common.Application.MediatR.Messaging;

namespace Identity.Application.UserRole.Commands;

public class CreateUserRoleCommand : ICommand<Guid>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
