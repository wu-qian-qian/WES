using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Wcs.Domain.Entities;
using Wcs.Domain.Repositories;
namespace Wcs.Infrastructure.DataBase.Repositories;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IWcsTaskInfoDetailRepository))]
public class WcsTaskInfoDetailRepository : IEntityRepository<WcsTaskInfoDetail, WcsDBContext>, IWcsTaskInfoDetailRepository
{
    public WcsTaskInfoDetailRepository(WcsDBContext dbContext) : base(dbContext)
    {
    }
}