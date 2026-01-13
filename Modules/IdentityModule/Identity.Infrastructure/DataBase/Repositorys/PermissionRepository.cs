using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain;

namespace Identity.Infrastructure.DataBase;
[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IPermissionRepository))]
public class PermissionRepository : IEntityRepository<Permission, IdentityDBContext>, IPermissionRepository
{
    public PermissionRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}