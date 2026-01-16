using System.Security.Claims;
using Common.Application.DecoratorEvent;
using Common.Domain.Entity;
using Common.Infrastructure.EF;
using Identity.Domain;
using Identity.Domain.Entities;
using Identity.Infrastructure.DataBase.EFExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Identity.Infrastructure.DataBase;

public class IdentityDBContext : BaseDbContext
{
    public const string SchemasTable = "Identity";


    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityDBContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor,
        IDomainEventDispatcher domainEventDispatcher) : base(options, domainEventDispatcher)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<Menu> Menus { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<PermissionMenu> PermissionMenus { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //关联关系配置
        modelBuilder.UserConfiguration();
        modelBuilder.RoleConfiguration();
        modelBuilder.PermissionConfiguration();
        modelBuilder.PermissionMenuConfiguration();
        modelBuilder.RolePermissionConfiguration();
        modelBuilder.MenuConfiguration();
        modelBuilder.UserRoleConfiguration();
    }

    protected override Task PreprocessEntities(IEnumerable<EntityEntry<BaseEntity>> entityEntries)
    {
        //当非API 接口调用是HttpContext为null 所以需要注意
        var userName = string.Empty;
        if (_httpContextAccessor.HttpContext == null)
        {
            if (_httpContextAccessor?.HttpContext?.User != null &&
                _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                // 优先用 FindFirst (比 FirstOrDefault 更贴合Claims查询语义)
                var nameClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name);
                userName = nameClaim?.Value ?? string.Empty;
            }

            // 未获取到用户时的默认值（避免实体字段为null）
            if (string.IsNullOrWhiteSpace(userName)) userName = "Anonymous"; // 匿名用户标识，或根据业务定义（如系统账号）
        }
        else
        {
            userName = "System";
        }

        foreach (var entity in entityEntries) entity.Entity.SetLastModification(userName);
        return base.PreprocessEntities(entityEntries);
    }
}