using Common.Presentation;
using Identity.Application.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.User;

internal class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("user/{id:guid}", async (ISender sender, Guid id) =>
        {
            var query = new GetUserQuery { Id = id };
            var result = await sender.Send(query);
            return result;
        }).WithTags(AssemblyReference.User);
    }
}
