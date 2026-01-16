using Common.Infrastructure.EF;
using S7.Domain;
namespace S7.Infrastructure.DataBase;

public class PlcEntityRepository : IEntityRepository<PlcEntityItem, PLCDBContext>, IPlcEntityRepository
{
    public PlcEntityRepository(PLCDBContext dbContext) : base(dbContext)
    {
    }
}