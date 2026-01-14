using Common.Application.DecoratorEvent;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
        var hasHandlers = serviceCollection.Any(sd =>sd.ServiceType.IsGenericType &&
                sd.ServiceType.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>));
            //一定要有IDomainEventHandler实现类才进行装饰器注册
        if (hasHandlers)
        {
            serviceCollection.Decorate(typeof(IDomainEventHandler<>), typeof(IdempotentDomainEventHandler<>));
        }
        serviceCollection.AddScoped<IDomainEventDispatcher, MediatrDomainEventDispatcher>();
        return serviceCollection;
    }
}