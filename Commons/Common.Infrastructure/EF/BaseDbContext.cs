using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.DecoratorEvent;
using Common.Domain;

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

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        var domainEntities = ChangeTracker
            .Entries<IEntity>()
            .Where(x => x.Entity.GetDomainEvents().Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.GetDomainEvents())
            .ToList(); //加ToList()是为立即加载，否则会延迟执行，到foreach的时候已经被ClearDomainEvents()了

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvent());

        foreach (var domainEvent in domainEvents) await _domainEventDispatcher.Dispatch(domainEvent);
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        return result;
    }
}