using Common.Presentation;
using Identity.Application.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.User;

internal class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("user/{id:guid}", async (ISender sender, Guid id) =>
        {
            var command = new DeleteUserCommand { Id = id };
            await sender.Send(command);
            return Results.Ok();
        }).WithTags(AssemblyReference.User);
    }
}
