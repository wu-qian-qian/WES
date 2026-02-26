using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain.Entities;
using Identity.Domain.Repository;

namespace Identity.Infrastructure.DataBase.Repositorys;

[DIAttribute(DILifeTimeEnum.Scoped, typeof(IRolePermissionRepository))]
public class RolePermissionRepository : IEntityRepository<RolePermission, IdentityDBContext>, IRolePermissionRepository
{
    public RolePermissionRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}