using Common.Application.DecoratorEvent;
using Common.Domain.Entity;
using Common.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;

namespace S7.Infrastructure.DataBase
{
    public class PLCDBContext : BaseDbContext
    {
        public const string SchemasTable = "PLC";
        public PLCDBContext(DbContextOptions options,IDomainEventDispatcher domainEventDispatcher) 
        : base(options, domainEventDispatcher)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //关联关系配置
          
        }

        protected override Task PreprocessEntities(IEnumerable<EntityEntry<BaseEntity>> entityEntries)
        {
           
            return base.PreprocessEntities(entityEntries);
        }
    }
}
