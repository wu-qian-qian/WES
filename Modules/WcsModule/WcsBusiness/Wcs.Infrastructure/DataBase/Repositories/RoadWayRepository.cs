using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Wcs.Domain.Entities;
using Wcs.Domain.Repositories;
namespace Wcs.Infrastructure.DataBase.Repositories;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IRoadWayRepository))]
public class RoadWayRepository : IEntityRepository<RoadWay, WcsDBContext>, IRoadWayRepository
{
    public RoadWayRepository(WcsDBContext dbContext) : base(dbContext)
    {
    }
}