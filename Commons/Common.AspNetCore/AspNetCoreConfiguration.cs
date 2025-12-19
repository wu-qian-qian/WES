using Common.AspNetCore.Authentication;
using Common.AspNetCore.Authorization;
using Common.AspNetCore.SwaggerUI;
using Common.Infrastructure;
using Common.JsonExtension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.AspNetCore;

public static class AspNetCoreConfiguration
{
    public static WebApplicationBuilder AddAspNetCore(this WebApplicationBuilder app, Assembly[] assemblies)
    {
        // Add AspNetCore related services here
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
        services.AddInfrastructureConfiguration(assemblies);
        services.AddHttpContextAccessor();
        services.AddSwaggerUIConfiguration();
        return app;
    }

    public static IServiceCollection AddAuthenticationConfiguration(IServiceCollection services, JWTOptions option)
    {
        services.AddAuthenticationConfiguration(option);
        return services;
    }

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