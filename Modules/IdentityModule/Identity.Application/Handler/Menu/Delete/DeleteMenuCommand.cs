using Common.Application.MediatR.Messaging;

namespace Identity.Application.Menu.Commands;

public class DeleteMenuCommand : ICommand
{
    public Guid Id { get; set; }
}
