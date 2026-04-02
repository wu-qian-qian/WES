using Common.Presentation;
using Identity.Application.PermissionMenu.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.PermissionMenu;

internal class GetList : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("permission-menu", async (ISender sender) =>
        {
            var result = await sender.Send(new GetPermissionMenuListQuery());
            return result;
        }).WithTags(AssemblyReference.PermissionMenu);
    }
}
