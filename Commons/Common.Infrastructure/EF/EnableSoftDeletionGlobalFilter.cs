using System.Linq.Expressions;
using Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.EF;

public static class EnableSoftDeletionGlobalFilter
{
    public static void AddSoftDeletionGlobalFilter(this ModelBuilder modelBuilder)
    {
        var entityTypesHasSoftDeletion = modelBuilder.Model.GetEntityTypes()
            .Where(e => e.ClrType.IsAssignableTo(typeof(IEntity)));

        foreach (var entityType in entityTypesHasSoftDeletion)
        {
            var isDeletedProperty = entityType.FindProperty(nameof(IEntity.IsDeleted));
            var parameter = Expression.Parameter(entityType.ClrType, "p");
            var filter =
                Expression.Lambda(Expression.Not(Expression.Property(parameter, isDeletedProperty.PropertyInfo)),
                    parameter);
            entityType.SetQueryFilter(filter);
        }
    }
}