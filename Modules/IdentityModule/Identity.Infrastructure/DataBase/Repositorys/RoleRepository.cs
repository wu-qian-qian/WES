using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain;

namespace Identity.Infrastructure.DataBase;
[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IRoleRepository))]
public class RoleRepository : IEntityRepository<Role, IdentityDBContext>, IRoleRepository
{
    public RoleRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}