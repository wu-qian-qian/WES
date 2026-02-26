using Microsoft.EntityFrameworkCore;
using Wcs.Domain.Entities;

namespace Wcs.Infrastructure.DataBase;
public static class DomainConfiguration
{
    public static void DeviceConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(entity =>
        {
           entity.ToTable(nameof(Device), WcsDBContext.SchemasTable);
           entity.HasKey(e => e.Id);
           entity.HasIndex(e => e.Name);
           entity.Property(e => e.Name).IsRequired().HasMaxLength(32);
           entity.Property(e => e.DeviceConfiguration).IsRequired().HasMaxLength(256);
        });
    }
    public static void JobConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
           entity.ToTable(nameof(Job), WcsDBContext.SchemasTable);
           entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.JobName);
            entity.Property(e => e.JobName).IsRequired().HasMaxLength(32);
            entity.Property(e => e.JobType).IsRequired().HasMaxLength(32);
        });
    }
    
     public static void LocationConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
           entity.ToTable(nameof(Location), WcsDBContext.SchemasTable);
           entity.HasKey(e => e.Id);
           entity.HasIndex(e => e.Code);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(32);
            entity.Property(e => e.AreaCode).IsRequired().HasMaxLength(32);
        });
    }

    public static void TaskDetailConfigConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskTemplate>(entity =>
        {
           entity.ToTable(nameof(TaskTemplate), WcsDBContext.SchemasTable);
           entity.HasKey(e => e.Id);

           entity.HasIndex(e => e.TaskTemplateCode);
            entity.Property(e => e.TaskTemplateCode).IsRequired().HasMaxLength(32);
        });
    }

    public static void WcsConfigurationConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WcsConfiguration>(entity =>
        {
           entity.ToTable(nameof(WcsConfiguration), WcsDBContext.SchemasTable);
           entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Key);
            entity.Property(e => e.Key).IsRequired().HasMaxLength(64);
            entity.Property(e => e.Value).IsRequired().HasMaxLength(256);
        });
    }

    public static void WcsEventConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WcsEvent>(entity =>
        {
           entity.ToTable(nameof(WcsEvent), WcsDBContext.SchemasTable);
           entity.HasKey(e => e.Id);
            entity.Property(e => e.EventId).IsRequired();
            entity.Property(e => e.Content).IsRequired().HasMaxLength(256);
        });
    }

    public static void WcsTaskInfoConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WcsTaskInfo>(entity =>
        {
           entity.ToTable(nameof(WcsTaskInfo), WcsDBContext.SchemasTable);
           entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.TaskCode);
            entity.Property(e => e.TaskCode).IsRequired().HasMaxLength(32);
            entity.Property(e => e.ContainerCode).IsRequired().HasMaxLength(32);
            entity.Property(e => e.TaskTemplateCode).IsRequired().HasMaxLength(32);
            entity.HasMany(e => e.Details)
                .WithOne(d => d.WcsTaskInfo)
                .HasForeignKey(d => d.WcsTaskInfo)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e=>e.SerialNumber) .UseIdentityColumn(3000);

        });
    }

    public static void WcsTaskInfoDetailConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WcsTaskInfoDetail>(entity =>
        {
           entity.ToTable(nameof(WcsTaskInfoDetail), WcsDBContext.SchemasTable);
           entity.HasKey(e => e.Id);
           entity.Property(e => e.DeviceName).IsRequired().HasMaxLength(32);
        });
    }

     public static void RoadWayConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoadWay>(entity =>
        {
           entity.ToTable(nameof(RoadWay), WcsDBContext.SchemasTable);
           entity.HasKey(e => e.Id);
            entity.Property(e => e.CurrentDeviceCode).IsRequired().HasMaxLength(32);
            entity.Property(e => e.TargetDeviceCode).IsRequired().HasMaxLength(32);
            entity.Property(e => e.TargetDeviceCode).IsRequired().HasMaxLength(32);
                entity.HasOne(e => e.Region)
                    .WithMany()
                    .HasForeignKey(e => e.RegionId);
        });
    }

    public static void RegionConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Region>(entity =>
        {
           entity.ToTable(nameof(Region), WcsDBContext.SchemasTable);
           entity.HasKey(e => e.Id);
            entity.Property(e => e.RegionCode).IsRequired().HasMaxLength(32);
        });
    }
}