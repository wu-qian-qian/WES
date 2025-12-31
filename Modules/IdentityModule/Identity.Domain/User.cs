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
    public class User: DomainEntity
    {
        public User() : base(Guid.NewGuid())
        {
            IsEnabled = true;
        }

        public string Username { get; set; }
        public string PasswordHash { get; set; } 

        public string? Nickname { get; set; }

        public bool IsEnabled { get; set; }

        // 关联角色（多对多）
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
