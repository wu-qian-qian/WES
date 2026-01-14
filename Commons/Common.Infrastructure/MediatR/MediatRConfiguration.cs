using Common.Application.MediatR.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Infrastructure.MediatR;

public static class MediatRConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleAssemblies"></param>
    /// <param name="behaviors">自定义管道配置</param>
    /// <returns></returns>
    public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services,
        Assembly[] moduleAssemblies, Action<MediatRServiceConfiguration>[]? behaviors=null)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            if(behaviors!=null)
            {
                foreach (var behavior in behaviors)
                {
                    behavior.Invoke(config);
                }
            }
        });
        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);
        return services;
    }
}