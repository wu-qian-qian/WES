using System.Reflection;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.Presentation;

public static class PresentationConfinguration
{
    /// <summary>
    ///     注入API 端点
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection services, params Assembly[] assemblies)
    {
        var serviceDescriptors = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    /// <summary>
    ///     Maps the endpoints to the application builder.
    ///     将API端点映射到应用程序构建器。
    /// </summary>
    /// <param name="app"></param>
    /// <param name="routeGroupBuilder"></param>
    /// <returns></returns>
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var endpoint in endpoints)
            endpoint.MapEndpoint(builder);
        return app;
    }

    /// <summary>
    ///     /// 注入 MassTransit 端点
    ///     此方法的调用应放在程序启动时调用，不可放在模块的基础设施层调用；除非该模块为独立运行的服务。
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureBus">默认使用内存</param>
    /// <param name="moduleConfigureConsumers"></param>
    /// <returns></returns>
    public static IServiceCollection AddMassTransitEndpoints(this IServiceCollection services,
        Action<IBusRegistrationConfigurator> configureBus = null,
        params Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        if (configureBus == null)
            configureBus = cfg => { cfg.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); }); };
        services.AddMassTransit(busConfigurator =>
        {
            foreach (var moduleConfigureConsumer in moduleConfigureConsumers) moduleConfigureConsumer(busConfigurator);
            configureBus(busConfigurator);
        });
        return services;
    }
}