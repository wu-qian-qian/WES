using Common.Application.NET.Other.Base;
using Common.Infrastructure.Net.Other;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using S7.Infrastructure.DataBase;

public static class InfrastructureConfiguration
{
  public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services,
  IConfiguration configuration)
  {
     services.AddSingleton<INetService, NetService>();    
     services.AddDbContext<PLCDBContext>(options =>{
            var connStr = configuration.GetConnectionString("default");
            options.UseSqlServer(connStr, builder =>
            builder.MigrationsHistoryTable(PLCDBContext.SchemasTable+ HistoryRepository.DefaultTableName));
            });
     return services;
  }
}