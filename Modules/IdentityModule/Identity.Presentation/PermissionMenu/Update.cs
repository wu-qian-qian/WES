using Common.Presentation;
using Identity.Application.PermissionMenu.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.PermissionMenu;

internal class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("permission-menu/{id:guid}", async (ISender sender, Guid id, PermissionMenuRequest dto) =>
        {
            var result = await sender.Send(new UpdatePermissionMenuCommand
            {
                Id = id,
                PermissionId = dto.PermissionId,
                MenuId = dto.MenuId
            });

            if (!result.IsSuccess)
            {
                return Results.NotFound(result.Message);
            }

            return Results.Ok();
        }).WithTags(AssemblyReference.PermissionMenu);
    }
}
