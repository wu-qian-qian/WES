using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Wcs.Domain.Entities;
using Wcs.Domain.Repositories;
namespace Wcs.Infrastructure.DataBase.Repositories;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IWcsConfigurationRepository))]
public class WcsConfigurationRepository : IEntityRepository<WcsConfiguration, WcsDBContext>, IWcsConfigurationRepository
{
    public WcsConfigurationRepository(WcsDBContext dbContext) : base(dbContext)
    {
    }
}