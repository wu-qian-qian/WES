using Common.Application.MediatR.Messaging;

namespace Identity.Application.Permission.Queries;

public class GetPermissionQuery : IQuery<PermissionDto>
{
    public Guid Id { get; set; }
}
