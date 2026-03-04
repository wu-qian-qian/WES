using Common.Application.MediatR.Messaging;

namespace Identity.Application.User.Commands;

public class DeleteUserCommand : ICommand
{
    public Guid Id { get; set; }
}
