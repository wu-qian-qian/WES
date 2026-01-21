using Microsoft.Extensions.DependencyInjection;
using S7.Application.Services;
using S7.Infrastructure.Service;

namespace S7.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection descriptors)
    {
        descriptors.AddSingleton<IReadModelBuildService,ReadModelBuildService>();
        return descriptors;
    }
}