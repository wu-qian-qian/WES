using Common.Application.MediatR.Messaging;

namespace Identity.Application.Role.Queries;

public class GetRoleQuery : IQuery<RoleDto>
{
    public Guid Id { get; set; }
}
