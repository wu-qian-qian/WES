using System.Reflection;
using System.Text.Json.Serialization;
using Common.AspNetCore.Authentication;
using Common.AspNetCore.Authorization;
using Common.AspNetCore.HostedService;
using Common.AspNetCore.SwaggerUI;
using Common.Infrastructure;
using Common.JsonExtension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.AspNetCore;

/// <summary>
/// 只属Web配置
/// </summary>
public static class AspNetCoreConfiguration
{
    public static WebApplicationBuilder AddAspNetCore(this WebApplicationBuilder app,AspNetCoreOptions options,
    params Assembly[] applicationAss)
    {
        //Add AspNetCore related services here
        //json格式设置
        var services = app.Services;
        services.Configure<JsonOptions>(options =>
        {
            //设置时间格式。而非“2008-08-08T08:08:08”这样的格式
            options.SerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
            //字符串枚举兼容字符反序列化枚举
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        //跨域设置
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(configurePolicy =>
            {
                configurePolicy.WithMethods("PUT", "DELETE", "GET", "POST", "PATCH")
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
        //HttpContext 注入
        services.AddHttpContextAccessor();
        services.AddSwaggerUIConfiguration();
        AddAuthenticationConfiguration(services,options.JWTOptions.Issuer,options.JWTOptions.Audience,options.JWTOptions.Key);
        AddAuthorizationConfiguration(services,options.PermissionCode,options.AuthorizationAction);
        AddConfigurationService(services,options.Configurations);
        options.ModulesOption.ApplicationAssemblyArr = applicationAss;
        services.AddInfranstructureConfiguration(options.ModulesOption);
        return app;
    }

    /// <summary>
    ///     授权
    /// </summary>
    /// <param name="services"></param>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    private static IServiceCollection AddAuthenticationConfiguration(IServiceCollection services
        , string issuer, string audience, string key)
    {
        services.AddAuthenticationConfiguration(issuer, audience, key);
        //添加swagger报文头UI
        services.Configure<SwaggerGenOptions>(c => { c.AddSwaggerUIAuthorizationHeard(); });
        return services;
    }

    /// <summary>
    ///     权限
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="act"></param>
    /// <returns></returns>
    private static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection serviceCollection
    ,IEnumerable<string> permissionCodes,Action<AuthorizationOptions>? act = null)
    {
        if (act == null)
        {
            act = options =>
            {
              foreach(var item in permissionCodes)
                {
                options.AddPolicy(item, policy =>
                policy.Requirements.Add(new PermissionRequirement(item)));
                }
            };
            AuthorizationConfiguration.AddAuthorizationConfiguration(serviceCollection,act);
        }   
        return serviceCollection;
    }

    /// <summary>
    ///     配置服务
    ///     当程序被启动时，会依次执行传入的配置服务
    ///     主要用来初始化一些数据，相当于预加载
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurations"></param>
    /// <returns></returns>
    private static IServiceCollection AddConfigurationService(this IServiceCollection services,
        params Func<IServiceScope, Task>[] configurations)
    {
        if (configurations != null && configurations.Any())
        {
        var configurationService = new ConfigurationOptions();
        configurationService.AddConfiguration(configurations);
        services.AddSingleton(configurationService);
        services.AddHostedService<InitielizeConfigurationService>();
        }
        return services;
    }
}