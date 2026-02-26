using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Wcs.Domain.Entities;
using Wcs.Domain.Repositories;
namespace Wcs.Infrastructure.DataBase.Repositories;

[DIAttribute(DILifeTimeEnum.Scoped, typeof(IWcsEventRepository))]
public class WcsEventRepository : IEntityRepository<WcsEvent, WcsDBContext>, IWcsEventRepository
{
    public WcsEventRepository(WcsDBContext dbContext) : base(dbContext)
    {
    }
}