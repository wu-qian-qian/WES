using Common.Application.MediatR.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Infrastructure.MediatR;

internal static class MediatRConfiguration
{
    internal static IServiceCollection AddMediatRConfiguration(this IServiceCollection services,
        Assembly[] moduleAssemblies, Action<MediatRServiceConfiguration>[] behaviors)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            foreach (var action in behaviors) action(config);
        });
        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);
        return services;
    }
}