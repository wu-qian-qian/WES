using Identity.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.DataBase.EFExtension
{
    public static class DomainConfiguration
    {
        public static void UserConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(config =>
            {
                config.ToTable(IdentityDBContext.SchemasTable + nameof(Domain.User));
                config.HasKey(x => x.Id);
                config.Property(p => p.Username).IsRequired().HasMaxLength(24);
                config.HasIndex(p => p.Username);   //.IsClustered(); 个人人为该字段可以设置成聚集索引
                config.Property(p=>p.PasswordHash).IsRequired().HasMaxLength(64);
                config.Property(p=>p.Nickname).IsRequired().HasMaxLength(32);
            });
        }
        public static void RoleConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(config =>
            {
                config.ToTable(IdentityDBContext.SchemasTable + nameof(Domain.Role));
                config.HasKey(x => x.Id);
                config.Property(p => p.RoleName).IsRequired().HasMaxLength(24);
                config.HasIndex(p => p.RoleName);  
                config.Property(p => p.Description).IsRequired(false).HasMaxLength(64);
                config.HasMany(p=>p.RolePermissions)
                .WithOne().HasForeignKey(p=>p.RoleId);
            });
        }

        public static void UserRoleConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>(config =>
            {
                config.ToTable(IdentityDBContext.SchemasTable + nameof(Domain.UserRole));
                config.HasKey(x => x.Id);
                config.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Cascade);//级联关系角色被删除级联也被删除
                config.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);//级联关系角色被删除级联也被删除
            });
        }

        public static void PermissionConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>(config =>
            {
                config.Property(p => p.PermissionCode)
                 .IsRequired().HasMaxLength(16);
            });
        }

        public static void RolePermissionConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>(config =>
            {
                config.HasOne(rp => rp.Role)
                     .WithMany(r => r.RolePermissions)
                     .HasForeignKey(rp => rp.RoleId)
                     .OnDelete(DeleteBehavior.Cascade);

                config.HasOne(rp => rp.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(rp => rp.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public static void PermissionMenuConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionMenu>(config =>
            {
              config.HasOne(pm => pm.Permission)
                    .WithMany(p => p.PermissionMenus)
                    .HasForeignKey(pm => pm.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade);
              config.HasOne(pm => pm.Menu)
                    .WithMany(m => m.PermissionMenus)
                    .HasForeignKey(pm => pm.MenuId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public static void MenuConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>(config =>
            {
                config.Property(p => p.MenuName)
                .IsRequired().HasMaxLength(16);
                config.Property(p => p.Path)
                .IsRequired(false).HasMaxLength(32);
                config.Property(p => p.Icon)
               .IsRequired(false).HasMaxLength(32);
            });
        }
    }
}
