using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain.Entities;
using Identity.Domain.Repository;

namespace Identity.Infrastructure.DataBase.Repositorys;

[DIAttribute(DILifeTimeEnum.Scoped, typeof(IUserRoleRepository))]
public class UserRoleRepository : IEntityRepository<UserRole, IdentityDBContext>, IUserRoleRepository
{
    public UserRoleRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}