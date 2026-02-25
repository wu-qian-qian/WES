using Microsoft.EntityFrameworkCore;
using Wcs.Domain.Entities;

namespace Wcs.Infrastructure.DataBase;
public static class DomainConfiguration
{
    public static void DeviceConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(entity =>
        {
           
            
           
        });
    }
}