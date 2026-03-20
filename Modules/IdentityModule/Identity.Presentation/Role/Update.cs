using Common.Presentation;
using Identity.Application.Role.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Role;

internal class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("role/{id:guid}", async (ISender sender, Guid id, RoleRequest dto) =>
        {
            var command = new UpdateRoleCommand
            {
                Id = id,
                RoleName = dto.RoleName,
                Description = dto.Description
            };

            var result = await sender.Send(command);
            if (!result.IsSuccess)
            {
                return Results.NotFound(result.Message);
            }
            return Results.Ok();
        }).WithTags(AssemblyReference.Role);
    }
}
