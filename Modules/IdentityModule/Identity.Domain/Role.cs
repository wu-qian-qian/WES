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

        public string RoleName { get; set; }
        public string? Description { get; set; }

        // 关联用户
        public ICollection<UserRole> UserRoles { get; set; }

        // 关联权限
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
