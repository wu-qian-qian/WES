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
using Common.Infrastructure.MediatR;
using Common.Infrastructure.DecoratorEvent;

namespace Identity.Infrastructure
{
    public static class InfranstructureConfiguration
    {
        public static IServiceCollection AddInfranstructureConfiguration(this  IServiceCollection services
        , IConfiguration configuration)
        {
             services.AddDecoratorConfiguration( [typeof(IdempotentDomainEventHandler<>)], typeof(ApplicationConfiguration).Assembly);
            services.AddDependyConfiguration([typeof(InfranstructureConfiguration).Assembly]);
            services.AddDbContext<IdentityDBContext>(options =>{
            var connStr = configuration.GetConnectionString("default");
            options.UseSqlServer(connStr, builder =>
            builder.MigrationsHistoryTable(IdentityDBContext.SchemasTable+ HistoryRepository.DefaultTableName));
            });
            services.AddMediatRConfiguration([typeof(ApplicationConfiguration).Assembly]);
            services.AddEndpoints(typeof(AssemblyReference).Assembly);
            return services;
        }


}
}
