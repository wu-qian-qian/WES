using Common.Domain.Entity;

namespace Common.Domain.Repository;

public interface IRepository<T> where T : IEntity<Guid>
{
    public ValueTask<T?> GetAsync(Guid id);

    public ValueTask UpdatesAsync(params T[] entity);

    public ValueTask DeletesAsync(params T[] entity);

    public ValueTask InserAsync(params T[] entity);
    public ValueTask<IQueryable<T>> GetQueryableAsync(bool asNoTrack = true);
}