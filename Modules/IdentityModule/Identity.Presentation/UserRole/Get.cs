using Common.Presentation;
using Identity.Application.UserRole.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.UserRole;

internal class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("user-role/{id:guid}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new GetUserRoleQuery { Id = id });
            return result;
        }).WithTags(AssemblyReference.UserRole);
    }
}
