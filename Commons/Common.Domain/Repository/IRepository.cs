using Common.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Repository
{
    public interface IRepository<T> where T : IEntity<Guid>
    {
        public ValueTask<T?> GetAsync(Guid id);

        public ValueTask UpdatesAsync(params T[] entity);

        public ValueTask DeletesAsync(params T[] entity);

        public  ValueTask InserAsync(params T[] entity);
        public  ValueTask<IQueryable<T>> GetQueryableAsync(bool asNoTrack = true);
    }
}
