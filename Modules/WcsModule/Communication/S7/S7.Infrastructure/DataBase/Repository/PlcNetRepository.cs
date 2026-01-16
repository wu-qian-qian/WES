using Common.Infrastructure.EF;
using S7.Domain.Entities;
using S7.Domain.Repository;

namespace S7.Infrastructure.DataBase.Repository;

public class PlcNetRepository : IEntityRepository<PlcNetConfig, PLCDBContext>, IPlcNetRepository
{
    public PlcNetRepository(PLCDBContext dbContext) : base(dbContext)
    {
    }
}