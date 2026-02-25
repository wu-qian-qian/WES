using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Wcs.Domain.Entities;
using Wcs.Domain.Repositories;
namespace Wcs.Infrastructure.DataBase.Repositories;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(ITaskDetailConfigRepository))]
public class TaskDetailConfigRepository : IEntityRepository<TaskDetailConfig, WcsDBContext>, ITaskDetailConfigRepository
{
    public TaskDetailConfigRepository(WcsDBContext dbContext) : base(dbContext)
    {
    }
}