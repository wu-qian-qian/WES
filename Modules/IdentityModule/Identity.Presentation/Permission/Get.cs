using Common.Presentation;
using Identity.Application.Permission.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Permission;

internal class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("permission/{id:guid}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new GetPermissionQuery { Id = id });
            if (!result.IsSuccess)
            {
                return Results.NotFound(result.Message);
            }

            return Results.Ok(result.Value);
        }).WithTags(AssemblyReference.Permission);
    }
}
