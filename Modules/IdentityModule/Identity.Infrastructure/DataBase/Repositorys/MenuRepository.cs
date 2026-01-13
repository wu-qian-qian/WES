using Common.Infrastructure.Attributes;
using Common.Infrastructure.EF;
using Common.Infrastructure.Enums;
using Identity.Domain;

namespace Identity.Infrastructure.DataBase;

[DIAttrubite(DILifeTimeEnum.Scoped, typeof(IMenuRepository))]
public class MenuRepository : IEntityRepository<Menu, IdentityDBContext>, IMenuRepository
{
    public MenuRepository(IdentityDBContext dbContext) : base(dbContext)
    {
    }
}