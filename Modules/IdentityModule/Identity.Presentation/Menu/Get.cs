using Common.Presentation;
using Identity.Application.Menu.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Menu;

internal class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("menu/{id:guid}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new GetMenuQuery { Id = id });
            if (!result.IsSuccess)
            {
                return Results.NotFound(result.Message);
            }

            return Results.Ok(result.Value);
        }).WithTags(AssemblyReference.Menu);
    }
}
