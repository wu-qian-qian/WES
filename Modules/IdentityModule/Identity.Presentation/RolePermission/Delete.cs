using Common.Presentation;
using Identity.Application.RolePermission.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.RolePermission;

internal class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("role-permission/{id:guid}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new DeleteRolePermissionCommand { Id = id });
            return result;
        }).WithTags(AssemblyReference.RolePermission);
    }
}
