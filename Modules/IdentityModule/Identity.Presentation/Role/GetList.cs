using Common.Presentation;
using Identity.Application.Role.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Role;

internal class GetList : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("role", async (ISender sender) =>
        {
            var query = new GetRoleListQuery();
            var result = await sender.Send(query);
            return result;
        }).WithTags(AssemblyReference.Role);
    }
}
