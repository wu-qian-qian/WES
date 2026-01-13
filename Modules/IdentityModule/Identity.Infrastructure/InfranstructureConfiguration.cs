using Common.Infrastructure;
using Common.Infrastructure.DependencyInjection;
using Identity.Application;
using Identity.Infrastructure.DataBase;
using Identity.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure
{
    public static class InfranstructureConfiguration
    {
        public static IServiceCollection AddInfranstructureConfiguration(this  IServiceCollection services
        , IConfiguration configuration)
        {
            services.AddDependyConfiguration([typeof(InfranstructureConfiguration).Assembly]);
            services.AddDbContext<IdentityDBContext>(options =>{
            var connStr = configuration.GetConnectionString("default");
            options.UseSqlServer(connStr, builder =>
            builder.MigrationsHistoryTable(IdentityDBContext.SchemasTable+ HistoryRepository.DefaultTableName));
            });
            InfrastructureConfiguration.AddMediatRConfiguration(services,[typeof(ApplicationConfiguration).Assembly]);
            return services;
        }

/// <summary>
/// 测试环境下一些直接初始化的项
/// </summary>
/// <param name="scope"></param>
        public static void Initialize(IServiceScope scope)
        {
            MigrationExtensions.ApplyMigration<IdentityDBContext>(scope);
        }
    }
}
