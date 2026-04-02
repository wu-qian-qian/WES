using Common.Presentation;
using Identity.Application.Menu.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Menu;

internal class Add : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("menu", async (ISender sender, MenuRequest dto) =>
        {
            var result = await sender.Send(new CreateMenuCommand
            {
                MenuName = dto.MenuName ?? string.Empty,
                Path = dto.Path,
                Icon = dto.Icon,
                ParentId = dto.ParentId,
                Sort = dto.Sort ?? 0
            });
            return result;
        }).WithTags(AssemblyReference.Menu);
    }
}
