using Common.Domain;
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
    public class PermissionMenu:IEntity<Guid>
    {
        public PermissionMenu()
        {
            Id = Guid.NewGuid();
        }

        public int PermissionId { get; set; }

        public int MenuId { get; set; }

        public Permission Permission { get; set; }
        public Menu Menu { get; set; }
    }
}
