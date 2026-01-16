using Common.Domain.Entity;

namespace Identity.Domain.Entities;

public class Menu : IEntity<Guid>
{
    public Menu()
    {
        Id = Guid.NewGuid();
    }

    public string MenuName { get; set; }


    public string Path { get; set; }


    public string Icon { get; set; }

    public Guid? ParentId { get; set; }

    public int Sort { get; set; } = 0;

    public ICollection<PermissionMenu> PermissionMenus { get; set; }
}