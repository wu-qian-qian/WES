using Common.Presentation;
using Identity.Application.Menu.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Menu;

internal class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("menu/{id:guid}", async (ISender sender, Guid id, MenuRequest dto) =>
        {
            var result = await sender.Send(new UpdateMenuCommand
            {
                Id = id,
                MenuName = dto.MenuName,
                Path = dto.Path,
                Icon = dto.Icon,
                ParentId = dto.ParentId,
                Sort = dto.Sort
            });

            if (!result.IsSuccess)
            {
                return Results.NotFound(result.Message);
            }

            return Results.Ok();
        }).WithTags(AssemblyReference.Menu);
    }
}
