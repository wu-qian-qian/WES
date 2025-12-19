using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AspNetCore.Authentication;

internal static class AuthenticationConfiguration
{
    /// <summary>
    /// 认证对一些细节进行认证
    /// </summary>
    /// <param name="services"></param>
    /// <param name="option"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, JWTOptions option)
    {
        services.AddSingleton<JwtCodeService>();
        services.AddAuthentication("JwtBearer")
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = option.Issuer,
                    ValidateAudience = true,
                    ValidAudience = option.Audience,
                    ValidateLifetime = false,
                    RequireExpirationTime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(option.Key))
                };
                // 处理SignalR的特殊情况：Bearer Token可能通过查询字符串传递
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // 从查询字符串中获取access_token参数（SignalR默认将jwt字符放到access_token）
                        var accessToken = context.Request.Query["access_token"].ToString() ??
                                          context.Request.Headers.Authorization.ToString();
                        // 如果请求是针对SignalR集线器的
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken)) // 替换为你的Hub路径
                            //赋值这里鉴权中间件就可以获取到
                            context.Token = accessToken;
                        return Task.CompletedTask;
                    }
                };
            });
        return services;
    }

    /// <summary>
    /// 只对key进行确认其他不做处理
    /// </summary>
    /// <param name="services"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, string key)
    {
        services.AddSingleton<JwtCodeService>();
        services.AddAuthentication("JwtBearer")
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    RequireExpirationTime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(key))
                };
                // 处理SignalR的特殊情况：Bearer Token可能通过查询字符串传递
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // 从查询字符串中获取access_token参数（SignalR默认将jwt字符放到access_token）
                        var accessToken = context.Request.Query["access_token"].ToString() ??
                                          context.Request.Headers.Authorization.ToString();
                        // 如果请求是针对SignalR集线器的
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken)) // 替换为你的Hub路径
                            //赋值这里鉴权中间件就可以获取到
                            context.Token = accessToken;
                        return Task.CompletedTask;
                    }
                };
            });
        return services;
    }
}