using Common.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain
{
    public class RolePermission:IEntity<Guid>
    {
        public RolePermission()
        {
            Id=Guid.NewGuid();
        }

        public Guid RoleId { get; set; }

        public Guid PermissionId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
