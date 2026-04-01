using Common.Application.MediatR.Messaging;

namespace Identity.Application.Menu.Commands;

public class UpdateMenuCommand : ICommand
{
    public Guid Id { get; set; }
    public string? MenuName { get; set; }
    public string? Path { get; set; }
    public string? Icon { get; set; }
    public Guid? ParentId { get; set; }
    public int? Sort { get; set; }
}
