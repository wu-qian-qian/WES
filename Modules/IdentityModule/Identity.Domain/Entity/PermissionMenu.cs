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
    /// 中间表
    /// 用来连接权限和菜单的关联关系
    /// </summary>
    public class PermissionMenu:IEntity<Guid>
    {
        public PermissionMenu()
        {
            Id = Guid.NewGuid();
        }

        public Guid PermissionId { get; set; }

        public Guid MenuId { get; set; }

        public Permission Permission { get; set; }
        public Menu Menu { get; set; }
    }
}
