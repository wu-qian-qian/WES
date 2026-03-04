using Common.Application.MediatR.Messaging;

namespace Identity.Application.Role.Commands;

public class UpdateRoleCommand : ICommand
{
    public Guid Id { get; set; }
    public string? RoleName { get; set; }
    public string? Description { get; set; }
}
