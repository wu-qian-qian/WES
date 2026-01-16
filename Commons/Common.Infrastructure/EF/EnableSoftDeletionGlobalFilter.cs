using System.Linq.Expressions;
using Common.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.EF;

public static class EFExtension
{
    /// <summary>
    ///     启用软删除全局过滤器
    /// </summary>
    /// <param name="modelBuilder"></param>
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

    /// <summary>
    ///     为所有字符串类型设置默认最大长度
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="maxLength"></param>
    public static void AddStringMaxFilter(this ModelBuilder modelBuilder, int maxLength = 64)
    {
        var stringProperties = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(string) && p.GetMaxLength() == null);

        foreach (var property in stringProperties) property.SetMaxLength(maxLength);
    }
}