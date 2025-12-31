using Common.Domain.Entity;
using Common.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.EF
{
    public class IEntityRepository<T, TDbContext> where T :
     IEntity<Guid>
     where TDbContext : BaseDbContext
    {
        public IEntityRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected TDbContext DbContext { get; init; }

        public  ValueTask<T?> GetAsync(Guid id)
        {
             return  DbContext.Set<T>().FindAsync(id);      
        }

        public  ValueTask UpdatesAsync(params T[] entity)
        {
             DbContext.Set<T>().UpdateRange(entity);
            return ValueTask.CompletedTask;
        }

        public ValueTask DeletesAsync(params T[] entity)
        {
            DbContext.Set<T>().RemoveRange(entity);
            return ValueTask.CompletedTask;
        }

        public  async ValueTask InserAsync(params T[] entity)
        {
            await DbContext.Set<T>().AddRangeAsync(entity);
        }

        public virtual ValueTask<IQueryable<T>> GetQueryableAsync(bool asNoTrack = true)
        {
            IQueryable<T> querys = querys = DbContext.Set<T>().AsQueryable();
            if (asNoTrack)
            {
                querys.AsNoTracking();
            }
            return ValueTask.FromResult(querys);
        }
    }
}
