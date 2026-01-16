using System.Reflection;
using Common.Application.DecoratorEvent;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.DecoratorEvent;

public static class DecoratorConfiguration
{
    public static IServiceCollection AddDecoratorConfiguration(this IServiceCollection serviceCollection,
        params Assembly[] assArray)
    {
        serviceCollection.Scan(scan => scan.FromAssemblies(assArray)
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        var hasHandlers = serviceCollection.Any(sd => sd.ServiceType.IsGenericType &&
                                                      sd.ServiceType.GetGenericTypeDefinition() ==
                                                      typeof(IDomainEventHandler<>));
        //一定要有IDomainEventHandler实现类才进行装饰器注册
        if (hasHandlers)
            serviceCollection.Decorate(typeof(IDomainEventHandler<>), typeof(IdempotentDomainEventHandler<>));
        serviceCollection.AddScoped<IDomainEventDispatcher, MediatrDomainEventDispatcher>();
        return serviceCollection;
    }

    /// <summary>
    ///     注意 IDomainEventHandler 需要为public
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="decorateTypes">自定义的装饰器配置</param>
    /// <param name="assArray"></param>
    /// <returns></returns>
    public static IServiceCollection AddDecoratorConfiguration(this IServiceCollection serviceCollection,
        Type[] decorateTypes,
        params Assembly[] assArray)
    {
        serviceCollection.Scan(scan => scan.FromAssemblies(assArray)
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        // 2. 用幂等装饰器包装它们
        foreach (var item in decorateTypes) serviceCollection.Decorate(typeof(IDomainEventHandler<>), item);
        serviceCollection.AddScoped<IDomainEventDispatcher, MediatrDomainEventDispatcher>();
        return serviceCollection;
    }
}