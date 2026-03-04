namespace Common.Application.UnitOfWork;

/// <summary>
/// 工作单元
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}