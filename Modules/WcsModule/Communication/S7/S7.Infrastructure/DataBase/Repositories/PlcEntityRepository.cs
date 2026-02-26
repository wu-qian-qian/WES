using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using S7.Domain.Entities;
using S7.Domain.Repository;

namespace S7.Infrastructure.DataBase.Repository;

[DIAttribute(DILifeTimeEnum.Scoped, typeof(IPlcEntityRepository))]
public class PlcEntityRepository : IEntityRepository<PlcEntityItem, PLCDBContext>, IPlcEntityRepository
{
    public PlcEntityRepository(PLCDBContext dbContext) : base(dbContext)
    {
    }
}