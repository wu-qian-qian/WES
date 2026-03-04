using Common.Application.MediatR.Messaging;
using MediatR;

namespace Identity.Application.User;

public class CreateUserCommand : ICommand<Guid>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Nickname { get; set; }
    public bool IsEnabled { get; set; }
}
