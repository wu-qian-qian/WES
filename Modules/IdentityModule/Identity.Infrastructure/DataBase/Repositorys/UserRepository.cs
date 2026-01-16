using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain.Entities;
using Identity.Domain.Repository;

namespace Identity.Infrastructure.DataBase.Repositorys;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IUserRepository))]
public class UserRepository : IEntityRepository<User, IdentityDBContext>, IUserRepository
{
    public UserRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}