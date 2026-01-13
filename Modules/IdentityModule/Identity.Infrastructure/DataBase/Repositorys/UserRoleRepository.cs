using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain;

namespace Identity.Infrastructure.DataBase;
[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IUserRoleRepository))]
public class UserRoleRepository : IEntityRepository<UserRole, IdentityDBContext>, IUserRoleRepository
{
    public UserRoleRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}