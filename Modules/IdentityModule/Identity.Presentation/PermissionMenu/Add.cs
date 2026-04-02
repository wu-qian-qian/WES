using Common.Presentation;
using Identity.Application.PermissionMenu.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.PermissionMenu;

internal class Add : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("permission-menu", async (ISender sender, PermissionMenuRequest dto) =>
        {
            var result = await sender.Send(new CreatePermissionMenuCommand
            {
                PermissionId = dto.PermissionId,
                MenuId = dto.MenuId
            });
            return result;
        }).WithTags(AssemblyReference.PermissionMenu);
    }
}
