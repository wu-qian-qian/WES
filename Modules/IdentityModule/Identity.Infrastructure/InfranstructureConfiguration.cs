using Common.Infrastructure.DecoratorEvent;
using Common.Infrastructure.DependencyInjection;
using Common.Infrastructure.MediatR;
using Identity.Application;
using Identity.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class InfranstructureConfiguration
{
    /// <summary>
    ///     服务只有一个模块 可直接在基础设施层添加
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfranstructureConfiguration(this IServiceCollection services
        , IConfiguration configuration)
    {
        services.AddDecoratorConfiguration(typeof(ApplicationConfiguration).Assembly);
        services.AddDependyConfiguration([typeof(InfranstructureConfiguration).Assembly]);
        services.AddDbContext<IdentityDBContext>(options =>
        {
            var connStr = configuration.GetConnectionString("default");
            options.UseSqlServer(connStr, builder =>
                builder.MigrationsHistoryTable(IdentityDBContext.SchemasTable + HistoryRepository.DefaultTableName));
        });
        services.AddMediatRConfiguration([typeof(ApplicationConfiguration).Assembly]);
        return services;
    }
}