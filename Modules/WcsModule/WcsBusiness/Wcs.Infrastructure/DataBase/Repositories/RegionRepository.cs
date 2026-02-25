using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Wcs.Domain.Entities;
using Wcs.Domain.Repositories;
namespace Wcs.Infrastructure.DataBase.Repositories;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IRegionRepository))]
public class RegionRepository : IEntityRepository<Region, WcsDBContext>, IRegionRepository
{
    public RegionRepository(WcsDBContext dbContext) : base(dbContext)
    {
    }
}