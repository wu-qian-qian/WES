using Microsoft.Extensions.DependencyInjection;
using S7.Application.Abstractions;
using S7.Application.Services;
using S7.Infrastructure.S7Net;
using S7.Infrastructure.Service;

namespace S7.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection descriptors)
    {
        descriptors.AddSingleton<IReadModelBuildService,ReadModelBuildService>();
        descriptors.AddSingleton<IWriteModelBuildService,WriteModelBuildService>();
        descriptors.AddSingleton<IS7NetFactory,S7NetFactory>();
        return descriptors;
    }

/// <summary>
/// 程序加载的一些配置 
/// JOB加载
/// </summary>
/// <returns></returns>
    public static async Task LoadConfiguration()
    {
        
    }
}