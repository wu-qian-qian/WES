using Common.AspNetCore.Authentication;
using Common.AspNetCore.SwaggerUI;
using Common.JsonExtension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Common.AspNetCore;

public static class AspNetCoreConfiguration
{
    public static WebApplicationBuilder AddAspNetCore(this WebApplicationBuilder app)
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
        return app;
    }

    /// <summary>
    /// 授权
    /// </summary>
    /// <param name="services"></param>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuthenticationConfiguration(IServiceCollection services
        ,string issuer,string audience,string key)
    {
        services.AddAuthenticationConfiguration(issuer,audience,key);
        //添加swagger报文头UI
        services.Configure<SwaggerGenOptions>(c => { c.AddSwaggerUIAuthorizationHeard(); });
        return services;
    }

    /// <summary>
    /// 权限
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="act"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection serviceCollection,
        Action<AuthorizationOptions>? act = null)
    {
        if (act == null)
            act = options =>
            {
                //options.AddPolicy("ServiceA.Read", policy =>
                //policy.Requirements.Add(new PermissionRequirement("ServiceA.Read")));
            };
        serviceCollection.AddAuthorization(act);
        return serviceCollection;
    }
}
