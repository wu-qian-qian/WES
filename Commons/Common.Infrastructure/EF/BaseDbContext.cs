using Common.Application.DecoratorEvent;
using Common.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Common.Infrastructure.EF;

/// <summary>
///     优化项  AutoDetectChangesEnabled 属性  手动控制状态跟踪
/// </summary>
public abstract class BaseDbContext : DbContext
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public BaseDbContext(DbContextOptions options, IDomainEventDispatcher domainEventDispatcher) :
        base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddSoftDeletionGlobalFilter();
        modelBuilder.AddStringMaxFilter();
    }
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        var baseEntities = ChangeTracker.Entries<BaseEntity>()
              .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added).ToArray();

        var domainEvents = baseEntities.Where(e => e.Entity is DomainEntity)
             .Where(e => e.Entity is DomainEntity)
             .Select(p=>(DomainEntity)p.Entity)
             .SelectMany(x => x.GetDomainEvents())
           .ToArray(); //加ToList()是为立即加载，否则会延迟执行，到foreach的时候已经被ClearDomainEvents()了
        await PreprocessEntities(baseEntities);
        foreach (var domainEvent in domainEvents) await _domainEventDispatcher.Dispatch(domainEvent);
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //重置状态  一些特殊的场景如果不重置会造成状态污染
        ResetEntityStates();
        return result;
    }
    /// <summary>
    /// 重置实体状态（如你原逻辑，可选）
    /// </summary>
    private void ResetEntityStates()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            {
                entry.State = EntityState.Detached;
            }
        }
    }

    /// <summary>
    /// 审计日志 或者一些实体有细节需要处理专用
    /// </summary>
    /// <param name="entityEntries"></param>
    /// <returns></returns>
   protected virtual Task PreprocessEntities(IEnumerable<EntityEntry<BaseEntity>> entityEntries)
    {
        return Task.CompletedTask;
    }
}