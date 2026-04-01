using Common.Application.MediatR.Messaging;

namespace Identity.Application.UserRole.Commands;

public class UpdateUserRoleCommand : ICommand
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
