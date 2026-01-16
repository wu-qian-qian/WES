using Microsoft.EntityFrameworkCore;
using S7.Domain.Entities;

namespace S7.Infrastructure.DataBase.EFExtension;

public static class DomainConfiguration
{
    public static void PlcNetConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlcNetConfig>(entity =>
        {
            entity.ToTable(PLCDBContext.SchemasTable + nameof(PlcNetConfig));
            entity.HasIndex(p => p.Ip).IsUnique();
            entity.Property(p => p.Ip).HasMaxLength(15);
            entity.Property(p => p.ReadHeart).HasMaxLength(10);
            entity.Property(p => p.WriteHeart).HasMaxLength(10);
            entity.HasMany(p => p.PlcEntityItems)
                .WithOne().HasForeignKey(p => p.NetGuid);
            entity.Property(p => p.LastModifierUser).HasMaxLength(20);
        });
    }

    public static void PlcEntityItemConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlcEntityItem>(entity =>
        {
            entity.ToTable(PLCDBContext.SchemasTable + nameof(PlcEntityItem));
            entity.Property(e => e.Name).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Ip).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(p => p.LastModifierUser).HasMaxLength(20);
        });
    }
}