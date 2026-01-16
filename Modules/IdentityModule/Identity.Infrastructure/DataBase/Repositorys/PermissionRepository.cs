using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain.Entities;
using Identity.Domain.Repository;

namespace Identity.Infrastructure.DataBase.Repositorys;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IPermissionRepository))]
public class PermissionRepository : IEntityRepository<Permission, IdentityDBContext>, IPermissionRepository
{
    public PermissionRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}