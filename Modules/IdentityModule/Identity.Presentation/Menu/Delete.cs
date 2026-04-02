using Common.Presentation;
using Identity.Application.Menu.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Menu;

internal class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("menu/{id:guid}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new DeleteMenuCommand { Id = id });
            return result;
        }).WithTags(AssemblyReference.Menu);
    }
}
