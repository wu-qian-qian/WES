using Common.Infrastructure.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

public class AspNetCoreOptions
{
    public AspNetCoreOptions()
    {
        
    }
   #region 认证

/// <summary>
/// 权限编码
/// </summary>
   public IEnumerable<string> PermissionCode{get;set;}
   /// <summary>
   /// 权限配置
   /// </summary>
   public Action<AuthorizationOptions> AuthorizationAction;
 
   public JWTOptions JWTOptions{get;set;}
   #endregion

    #region   动态配置更新

     /// <summary>
     /// 动态事件
     /// </summary>
     public Func<IServiceScope, Task>[] Configurations;
    #endregion

    #region  Module 功能模块
    public ModulesOptions  ModulesOption{get;set;}
    #endregion
}