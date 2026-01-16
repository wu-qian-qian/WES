using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain.Entities;
using Identity.Domain.Repository;

namespace Identity.Infrastructure.DataBase.Repositorys;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IMenuRepository))]
public class MenuRepository : IEntityRepository<Menu, IdentityDBContext>, IMenuRepository
{
    public MenuRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}