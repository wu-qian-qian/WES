using Common.Presentation;
using Identity.Application.Permission.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Permission;

internal class GetList : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("permission", async (ISender sender) =>
        {
            var result = await sender.Send(new GetPermissionListQuery());
            return result;
        }).WithTags(AssemblyReference.Permission);
    }
}
