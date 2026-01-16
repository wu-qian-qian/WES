using Common.Infrastructure.EF;
using S7.Domain;
namespace S7.Infrastructure.DataBase;

public class PlcNetRepository : IEntityRepository<PlcNetConfig, PLCDBContext>,IPlcNetRepository
{
    public PlcNetRepository(PLCDBContext dbContext) : base(dbContext)
    {
    }
}