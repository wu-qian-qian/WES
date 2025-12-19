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
        // 2. 用幂等装饰器包装它们
        serviceCollection.Decorate(typeof(IDomainEventHandler<>), typeof(IdempotentDomainEventHandler<>));
        serviceCollection.AddScoped<IDomainEventDispatcher, MediatrDomainEventDispatcher>();
        return serviceCollection;
    }
}