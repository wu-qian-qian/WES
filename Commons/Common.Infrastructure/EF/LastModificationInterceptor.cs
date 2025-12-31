using Common.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.DataBase
{
   /// <summary>
   /// EF的拦截器
   /// </summary>
    public class LastModificationInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
            InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                //判断是否授权
              
                // 一次性筛选+缓存结果
                var modifiedEntities = eventData.Context.ChangeTracker.Entries<BaseEntity>()
                    // 仅筛选“修改/新增”状态（根据业务需求），排除无变化/已删除/分离的
                    .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added) // 接口约束，避免类型转换异常
                    .ToList();
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }


        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                //把被软删除的对象从Cache删除，否则FindAsync()还能根据Id获取到这条数据
                //因为FindAsync如果能从本地Cache找到，就不会去数据库上查询，而从本地Cache找的过程中不会管QueryFilter
                //就会造成已经软删除的数据仍然能够通过FindAsync查到的问题，因此这里把对应跟踪对象的state改为Detached，就会从缓存中删除了
                var softDeletedEntities = eventData.Context.ChangeTracker.Entries<BaseEntity>()
                    .Where(e => e.State == EntityState.Modified && e.Entity.IsDeleted)
                    .Select(e => e.Entity).ToList();

                softDeletedEntities.ForEach(e => eventData.Context.Entry(e).State = EntityState.Detached);
            }

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
