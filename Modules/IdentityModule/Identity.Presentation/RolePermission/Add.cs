using Common.Presentation;
using Identity.Application.RolePermission.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.RolePermission;

internal class Add : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("role-permission", async (ISender sender, RolePermissionRequest dto) =>
        {
            var result = await sender.Send(new CreateRolePermissionCommand
            {
                RoleId = dto.RoleId,
                PermissionId = dto.PermissionId
            });
            return result;
        }).WithTags(AssemblyReference.RolePermission);
    }
}
