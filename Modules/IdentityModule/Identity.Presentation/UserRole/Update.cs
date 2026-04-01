using Common.Presentation;
using Identity.Application.UserRole.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.UserRole;

internal class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("user-role/{id:guid}", async (ISender sender, Guid id, UserRoleRequest dto) =>
        {
            var result = await sender.Send(new UpdateUserRoleCommand
            {
                Id = id,
                UserId = dto.UserId,
                RoleId = dto.RoleId
            });

            if (!result.IsSuccess)
            {
                return Results.NotFound(result.Message);
            }

            return Results.Ok();
        }).WithTags(AssemblyReference.UserRole);
    }
}
