using Common.Presentation;
using Identity.Application.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.User;

internal class GetList : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("user", async (ISender sender) =>
        {
            var query = new GetUserListQuery();
            var result = await sender.Send(query);
            return result;
        }).WithTags(AssemblyReference.User);
    }
}
