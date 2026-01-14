using Common.Infrastructure;
using Common.Infrastructure.DependencyInjection;
using Identity.Application;
using Identity.Infrastructure.DataBase;
using Identity.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.Presentation;

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
            services.AddMediatRConfiguration(services,[typeof(ApplicationConfiguration).Assembly]);
            services.AddEndpoints(typeof(AssemblyReference).Assembly);
            return services;
        }


}
}
