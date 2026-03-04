using Common.Presentation;
using Identity.Application.Role.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Role;

internal class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("role/{id:guid}", async (ISender sender, Guid id) =>
        {
            var query = new GetRoleQuery { Id = id };
            var result = await sender.Send(query);
            if (!result.IsSuccess)
            {
                return Results.NotFound(result.Message);
            }
            return Results.Ok(result.Value);
        }).WithTags(AssemblyReference.Role);
    }
}
