using Common.Infrastructure.EF;
using S7.Domain.Entities;
using S7.Domain.Repository;

namespace S7.Infrastructure.DataBase.Repository;

public class PlcEntityRepository : IEntityRepository<PlcEntityItem, PLCDBContext>, IPlcEntityRepository
{
    public PlcEntityRepository(PLCDBContext dbContext) : base(dbContext)
    {
    }
}