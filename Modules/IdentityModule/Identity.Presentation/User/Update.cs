using Common.Presentation;
using Identity.Application.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.User;

internal class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("user/{id:guid}", async (ISender sender, Guid id, UserRequest dto) =>
        {
            var command = new UpdateUserCommand
            {
                Id = id,
                Username = dto.Username,
                Password = dto.Password,
                Nickname = dto.Nickname,
                IsEnabled = dto.IsEnabled
            };

            var result = await sender.Send(command);
            return result;
        }).WithTags(AssemblyReference.User);
    }
}
