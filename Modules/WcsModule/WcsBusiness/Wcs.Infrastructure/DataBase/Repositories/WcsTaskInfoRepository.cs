using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Wcs.Domain.Entities;
using Wcs.Domain.Repositories;
namespace Wcs.Infrastructure.DataBase.Repositories;

[DIAttribute(DILifeTimeEnum.Scoped, typeof(IWcsTaskInfoRepository))]
public class WcsTaskInfoRepository : IEntityRepository<WcsTaskInfo, WcsDBContext>, IWcsTaskInfoRepository
{
    public WcsTaskInfoRepository(WcsDBContext dbContext) : base(dbContext)
    {
    }
}