using Common.Domain;
using Common.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain
{
    public class Role:BaseEntity
    {
        public Role() 
        {
            Id=Guid.NewGuid();
        }

        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public bool IsEnabled { get; set; } = true;

        // 关联用户
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        // 关联权限
        public ICollection<Permission> RolePermissions { get; set; } = new List<Permission>();
    }
}
