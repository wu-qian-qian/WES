using Common.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Common.Infrastructure.EF;

public static class EnableSoftDeletionGlobalFilter
{
    public static void AddSoftDeletionGlobalFilter(this ModelBuilder modelBuilder)
    {
        var entityTypesHasSoftDeletion = modelBuilder.Model.GetEntityTypes()
            .Where(e => e.ClrType.IsAssignableTo(typeof(BaseEntity)));

        foreach (var entityType in entityTypesHasSoftDeletion)
        {
            var isDeletedProperty = entityType.FindProperty(nameof(BaseEntity.IsDeleted));
            var parameter = Expression.Parameter(entityType.ClrType, "p");
            var filter =
                Expression.Lambda(Expression.Not(Expression.Property(parameter, isDeletedProperty.PropertyInfo)),
                    parameter);
            entityType.SetQueryFilter(filter);
        }
    }
}