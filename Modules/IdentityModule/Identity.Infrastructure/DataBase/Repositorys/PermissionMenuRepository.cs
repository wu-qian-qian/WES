using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain.Entities;
using Identity.Domain.Repository;

namespace Identity.Infrastructure.DataBase.Repositorys;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IPermissionMenuRepository))]
public class PermissionMenuRepository : IEntityRepository<PermissionMenu, IdentityDBContext>, IPermissionMenuRepository
{
    public PermissionMenuRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}