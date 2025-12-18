using System.Reflection;
using Common.Application.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Common.AspNetCore.DependencyInjection;

public static class AttrubiteInjectionService
{
    public static IServiceCollection DependyConfiguration(this IServiceCollection services, Assembly[] assemblies)
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