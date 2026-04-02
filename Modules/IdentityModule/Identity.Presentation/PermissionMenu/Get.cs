using Common.Presentation;
using Identity.Application.PermissionMenu.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.PermissionMenu;

internal class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("permission-menu/{id:guid}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new GetPermissionMenuQuery { Id = id });
            return result;
        }).WithTags(AssemblyReference.PermissionMenu);
    }
}
