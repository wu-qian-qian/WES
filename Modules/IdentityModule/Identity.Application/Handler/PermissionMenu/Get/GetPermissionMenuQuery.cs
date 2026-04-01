using Common.Application.MediatR.Messaging;

namespace Identity.Application.PermissionMenu.Queries;

public class GetPermissionMenuQuery : IQuery<PermissionMenuDto>
{
    public Guid Id { get; set; }
}
