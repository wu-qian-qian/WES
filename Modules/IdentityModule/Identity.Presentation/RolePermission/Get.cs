using Common.Presentation;
using Identity.Application.RolePermission.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.RolePermission;

internal class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("role-permission/{id:guid}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new GetRolePermissionQuery { Id = id });
            return result;
        }).WithTags(AssemblyReference.RolePermission);
    }
}
