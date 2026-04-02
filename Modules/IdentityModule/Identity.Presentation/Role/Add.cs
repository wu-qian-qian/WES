using Common.Presentation;
using Identity.Application.Role.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Role;

internal class Add : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("role", async (ISender sender, RoleRequest dto) =>
        {
            var command = new CreateRoleCommand
            {
                RoleName = dto.RoleName,
                Description = dto.Description
            };

            var result = await sender.Send(command);
            return result;
        }).WithTags(AssemblyReference.Role);
    }
}
