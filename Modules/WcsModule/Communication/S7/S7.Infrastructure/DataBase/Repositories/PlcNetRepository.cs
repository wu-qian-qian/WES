using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using S7.Domain.Entities;
using S7.Domain.Repository;

namespace S7.Infrastructure.DataBase.Repository;

[DIAttribute(DILifeTimeEnum.Scoped, typeof(IPlcNetRepository))]
public class PlcNetRepository : IEntityRepository<PlcNetConfig, PLCDBContext>, IPlcNetRepository
{
    public PlcNetRepository(PLCDBContext dbContext) : base(dbContext)
    {
    }
}