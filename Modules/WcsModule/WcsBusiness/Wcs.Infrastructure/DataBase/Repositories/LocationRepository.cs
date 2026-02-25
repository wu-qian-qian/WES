using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Wcs.Domain.Entities;
using Wcs.Domain.Repositories;
namespace Wcs.Infrastructure.DataBase.Repositories;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(ILocationRepository))]
public class LocationRepository : IEntityRepository<Location, WcsDBContext>, ILocationRepository
{
    public LocationRepository(WcsDBContext dbContext) : base(dbContext)
    {
    }
}