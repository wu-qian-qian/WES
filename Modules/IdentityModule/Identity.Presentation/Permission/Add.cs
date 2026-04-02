using Common.Presentation;
using Identity.Application.Permission.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Permission;

internal class Add : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("permission", async (ISender sender, PermissionRequest dto) =>
        {
            var result = await sender.Send(new CreatePermissionCommand
            {
                PermissionCode = dto.PermissionCode ?? string.Empty
            });
            return result;
        }).WithTags(AssemblyReference.Permission);
    }
}
