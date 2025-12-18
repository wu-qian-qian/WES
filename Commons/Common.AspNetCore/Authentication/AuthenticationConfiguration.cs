using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Common.AspNetCore.Authentication
{
    internal static class AuthenticationConfiguration
    {
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services,JWTOptions option)
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
                        RequireExpirationTime=false,
                        ValidateIssuerSigningKey = true, 
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(option.Key))
                    };
                });
            return services;
        }
        
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services,string key)
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
                        RequireExpirationTime=false,
                        ValidateIssuerSigningKey = true, 
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(key))
                    };
                });
            return services;
        }
    }
}
