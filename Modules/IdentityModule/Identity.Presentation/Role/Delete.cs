using Common.Presentation;
using Identity.Application.Role.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Role;

internal class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("role/{id:guid}", async (ISender sender, Guid id) =>
        {
            var command = new DeleteRoleCommand { Id = id };
            var result = await sender.Send(command);
            if (!result.IsSuccess)
            {
                return Results.NotFound(result.Message);
            }
            return Results.Ok();
        }).WithTags(AssemblyReference.Role);
    }
}
