using Common.Presentation;
using Identity.Application.Permission.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Permission;

internal class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("permission/{id:guid}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new DeletePermissionCommand { Id = id });
            if (!result.IsSuccess)
            {
                return Results.NotFound(result.Message);
            }

            return Results.Ok();
        }).WithTags(AssemblyReference.Permission);
    }
}
