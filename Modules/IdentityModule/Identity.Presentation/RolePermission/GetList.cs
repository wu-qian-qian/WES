using Common.Presentation;
using Identity.Application.RolePermission.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.RolePermission;

internal class GetList : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("role-permission", async (ISender sender) =>
        {
            var result = await sender.Send(new GetRolePermissionListQuery());
            if (!result.IsSuccess)
            {
                return Results.Problem(result.Message);
            }

            return Results.Ok(result.Value);
        }).WithTags(AssemblyReference.RolePermission);
    }
}
