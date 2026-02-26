using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Wcs.Domain.Entities;
using Wcs.Domain.Repositories;
namespace Wcs.Infrastructure.DataBase.Repositories;

[DIAttribute(DILifeTimeEnum.Scoped, typeof(IJobRepository))]
public class JobRepository : IEntityRepository<Job, WcsDBContext>, IJobRepository
{
    public JobRepository(WcsDBContext dbContext) : base(dbContext)
    {
    }
}