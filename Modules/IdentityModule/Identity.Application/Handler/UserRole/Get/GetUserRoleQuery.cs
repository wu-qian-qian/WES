using Common.Application.MediatR.Messaging;

namespace Identity.Application.UserRole.Queries;

public class GetUserRoleQuery : IQuery<UserRoleDto>
{
    public Guid Id { get; set; }
}
