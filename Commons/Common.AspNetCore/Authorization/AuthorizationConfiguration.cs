using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Common.AspNetCore.Authorization;

public static class AuthorizationConfiguration
{
    /// <summary>
    /// 鉴权的注入
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="act">鉴权细节处理</param>
    /// <returns></returns>
    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection serviceCollection,
        Action<AuthorizationOptions>? act = null)
    {
        serviceCollection.AddAuthorization(act ?? throw new ArgumentNullException(nameof(act)));
        serviceCollection.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        return serviceCollection;
    }
}