using Common.Application.MediatR.Messaging;

namespace Identity.Application.User.Commands;

public class UpdateUserCommand : ICommand
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Nickname { get; set; }
    public bool? IsEnabled { get; set; }
}
