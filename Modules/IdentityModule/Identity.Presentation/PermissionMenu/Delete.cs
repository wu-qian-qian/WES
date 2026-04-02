using Common.Presentation;
using Identity.Application.PermissionMenu.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.PermissionMenu;

internal class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("permission-menu/{id:guid}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new DeletePermissionMenuCommand { Id = id });
            return result;
        }).WithTags(AssemblyReference.PermissionMenu);
    }
}
