using Common.Application.DecoratorEvent;
using Common.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Common.AspNetCore.EF;

public class AspNetCoreDbContext : BaseDbContext
{
    private readonly LastModificationInterceptor _lastModificationInterceptor;
    public AspNetCoreDbContext(DbContextOptions options, LastModificationInterceptor lastModificationInterceptor,
        IDomainEventDispatcher domainEventDispatcher) : base(options, domainEventDispatcher)
    {
        _lastModificationInterceptor = lastModificationInterceptor ??
                                       throw new ArgumentNullException(nameof(lastModificationInterceptor));
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        //拦截添加
        optionsBuilder.AddInterceptors(_lastModificationInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddSoftDeletionGlobalFilter();
    }
}