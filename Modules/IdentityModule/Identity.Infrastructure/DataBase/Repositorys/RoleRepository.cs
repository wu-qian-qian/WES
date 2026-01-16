using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain.Entities;
using Identity.Domain.Repository;

namespace Identity.Infrastructure.DataBase.Repositorys;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IRoleRepository))]
public class RoleRepository : IEntityRepository<Role, IdentityDBContext>, IRoleRepository
{
    public RoleRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}