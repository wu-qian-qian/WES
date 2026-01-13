using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain;

namespace Identity.Infrastructure.DataBase;
[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IRolePermissionRepository))]
public class RolePermissionRepository : IEntityRepository<RolePermission, IdentityDBContext>, IRolePermissionRepository
{
    public RolePermissionRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}