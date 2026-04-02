using Common.Presentation;
using Identity.Application.UserRole.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.UserRole;

internal class GetList : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("user-role", async (ISender sender) =>
        {
            var result = await sender.Send(new GetUserRoleListQuery());
            return result;
        }).WithTags(AssemblyReference.UserRole);
    }
}
