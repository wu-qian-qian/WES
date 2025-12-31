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
    public class Permission:IEntity<Guid>
    {
        public Permission() 
        {
            Id = Guid.NewGuid();
        }

        public int RoleId { get; set; }

        public string PermissionCode { get; set;  }

        public ICollection<PermissionMenu> PermissionMenus { get; set; } = new List<PermissionMenu>();
    }
}
