using System.Reflection;
using Common.Infrastructure.Attributes;
using Common.Infrastructure.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.DependencyInjection;

public static class AttrubiteInjectionService
{
    /// <summary>
    ///     特性自动注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AddDependyConfiguration(this IServiceCollection services, Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        foreach (var type in assembly.GetTypes().Where(p => p.IsClass && !p.IsAbstract))
        {
            var att = type.GetCustomAttribute<DIAttrubite>();
            if (att != null)
                switch (att.LifeTime)
                {
                    case DILifeTimeEnum.Scoped:
                        services.AddScoped(att.BaseType, type);
                        break;
                    case DILifeTimeEnum.Singleton:
                        services.AddSingleton(att.BaseType, type);
                        break;
                    case DILifeTimeEnum.Transient:
                        services.AddTransient(att.BaseType, type);
                        break;
                }
        }

        return services;
    }
}