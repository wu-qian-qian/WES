using Common.Application.MediatR.Messaging;

namespace Identity.Application.Role.Commands;

public class DeleteRoleCommand : ICommand
{
    public Guid Id { get; set; }
}
