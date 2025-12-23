using Microsoft.AspNetCore.Routing;

namespace Common.Presentation;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}