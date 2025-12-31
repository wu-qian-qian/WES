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
    public class Menu : IEntity<Guid>
    {
        public Menu() 
        {
            Id=Guid.NewGuid();
        }

        public string MenuName { get; set; } = string.Empty;


        public string? Path { get; set; }


        public string? Icon { get; set; }

        public int ParentId { get; set; } = 0;

        public int Sort { get; set; } = 0;

        public ICollection<PermissionMenu> PermissionMenus { get; set; }
    }
}
