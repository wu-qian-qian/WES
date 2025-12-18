using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace Common.AspNetCore.SwaggerUI
{
    internal static class SwaggerUIConfiguration
    {
        public static IServiceCollection AddSwaggerUIConfiguration(this IServiceCollection services
        ,string title="Swagger.UI",string version="V1.1",string desc="API UI")
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = title,
                    Version = version,
                    Description = desc
                });

                options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
            });
            return services;
        }
        
        /// <summary>
        /// SwaggerUI 添加鉴权请求
        /// </summary>
        /// <param name="c"></param>
        public  static void  AddSwaggerUIAuthorizationHeard(this SwaggerGenOptions c)
        {
            c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
            {
                Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Authorization"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Authorization"
                        },
                        Scheme = "oauth2",
                        Name = "Authorization",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        }
    }
}
