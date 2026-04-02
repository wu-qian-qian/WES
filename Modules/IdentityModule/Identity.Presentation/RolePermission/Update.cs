using Common.Presentation;
using Identity.Application.RolePermission.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.RolePermission;

internal class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("role-permission/{id:guid}", async (ISender sender, Guid id, RolePermissionRequest dto) =>
        {
            var result = await sender.Send(new UpdateRolePermissionCommand
            {
                Id = id,
                RoleId = dto.RoleId,
                PermissionId = dto.PermissionId
            });
            return result;
        }).WithTags(AssemblyReference.RolePermission);
    }
}
