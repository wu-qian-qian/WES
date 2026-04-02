using Common.Presentation;
using Identity.Application.UserRole.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.UserRole;

internal class Add : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user-role", async (ISender sender, UserRoleRequest dto) =>
        {
            var result = await sender.Send(new CreateUserRoleCommand
            {
                UserId = dto.UserId,
                RoleId = dto.RoleId
            });
            return result;
        }).WithTags(AssemblyReference.UserRole);
    }
}
