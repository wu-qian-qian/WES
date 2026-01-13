using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain;

namespace Identity.Infrastructure.DataBase;
[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IUserRepository))]
public class UserRepository : IEntityRepository<User, IdentityDBContext>, IUserRepository
{
    public UserRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}