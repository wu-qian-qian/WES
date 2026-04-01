using Common.Application.MediatR.Messaging;

namespace Identity.Application.Menu.Commands;

public class CreateMenuCommand : ICommand<Guid>
{
    public string MenuName { get; set; } = string.Empty;
    public string? Path { get; set; }
    public string? Icon { get; set; }
    public Guid? ParentId { get; set; }
    public int Sort { get; set; }
}
