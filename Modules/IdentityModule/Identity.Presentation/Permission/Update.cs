using Common.Presentation;
using Identity.Application.Permission.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Permission;

internal class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("permission/{id:guid}", async (ISender sender, Guid id, PermissionRequest dto) =>
        {
            var result = await sender.Send(new UpdatePermissionCommand
            {
                Id = id,
                PermissionCode = dto.PermissionCode
            });
            return result;
        }).WithTags(AssemblyReference.Permission);
    }
}
