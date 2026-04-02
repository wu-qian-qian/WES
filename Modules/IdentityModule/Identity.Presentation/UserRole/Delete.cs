using Common.Presentation;
using Identity.Application.UserRole.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.UserRole;

internal class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("user-role/{id:guid}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new DeleteUserRoleCommand { Id = id });
            return result;
        }).WithTags(AssemblyReference.UserRole);
    }
}
