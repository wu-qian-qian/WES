using Common.Presentation;
using Identity.Application.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.User;

internal class Add : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user/add-user", async (ISender sender, UserRequest dto) => 
        {
            var command = new CreateUserCommand
            {
                Username = dto.Username,
                Password = dto.Password,
                Nickname = dto.Nickname,
                IsEnabled = dto.IsEnabled
            };

            var userId = await sender.Send(command);
            return Results.Ok(new { Id = userId });
        }).WithTags(AssemblyReference.User);
    }
}