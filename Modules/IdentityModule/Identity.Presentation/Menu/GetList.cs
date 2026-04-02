using Common.Presentation;
using Identity.Application.Menu.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Menu;

internal class GetList : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("menu", async (ISender sender) =>
        {
            var result = await sender.Send(new GetMenuListQuery());
            return result;
        }).WithTags(AssemblyReference.Menu);
    }
}
