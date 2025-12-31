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
    /// <summary>
    /// 用来绑定多对多的关系
    /// 1个角色会存在多个用户
    /// 1个用户也可能拥有多个角色身份
    /// </summary>
    public class UserRole : IEntity<Guid>
    {
        public UserRole()
        {
            Id=Guid.NewGuid();
        }
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        // 导航属性
        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
