using Common.Application.MediatR.Messaging;

namespace Identity.Application.UserRole.Commands;

public class DeleteUserRoleCommand : ICommand
{
    public Guid Id { get; set; }
}
