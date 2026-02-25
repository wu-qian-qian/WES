using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Wcs.Domain.Entities;
using Wcs.Domain.Repositories;
namespace Wcs.Infrastructure.DataBase.Repositories;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IDeviceRepository))]
public class DeviceRepository : IEntityRepository<Device, WcsDBContext>, IDeviceRepository
{
    public DeviceRepository(WcsDBContext dbContext) : base(dbContext)
    {
    }
}