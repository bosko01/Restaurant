using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace Infrastructure.Database.Enumerator
{
    public static class EnumExtensions
    {
        public static PropertyBuilder<TProperty> ConfigureEnum<TEntity, TProperty>(this EntityTypeBuilder<TEntity> builder,
                                                                           Expression<Func<TEntity, TProperty>> enumSelector)
    where TProperty : Enum
    where TEntity : class
        {
            var propertyName = ((MemberExpression)enumSelector.Body).Member.Name;

            var propertyBuilder = builder.Property(enumSelector)
                                         .HasConversion(inMemory => inMemory.ToString(),
                                                        dataBaseEntry => (TProperty)Enum.Parse(typeof(TProperty), dataBaseEntry));

            var availableValues = string.Join(',', Enum.GetNames(typeof(TProperty)).Select(enumValue => $"'{enumValue}'"));

            var constraintName = $"CK_{typeof(TEntity).Name}_{propertyName}";

            builder.HasCheckConstraint(constraintName,
                                       sql: $"{propertyName} IN ({availableValues})");

            return propertyBuilder;
        }

        public static PropertyBuilder<string> ConfigureEnumList<TEntity, TEnum>(this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, List<TEnum>>> enumListSelector)
        where TEnum : struct, Enum
        where TEntity : class
        {
            var propertyName = ((MemberExpression)enumListSelector.Body).Member.Name;

            var propertyBuilder = builder.Property<string>(propertyName)
                .HasConversion(new EnumListToStringConverter<TEnum>());

            var availableValues = string.Join(',', Enum.GetNames(typeof(TEnum)).Select(enumValue => $"'{enumValue}'"));

            var constraintName = $"CK_{typeof(TEntity).Name}_{propertyName}";

            builder.HasCheckConstraint(constraintName,
                sql: $"{propertyName} IS NULL OR {propertyName} LIKE '%{string.Join("%' OR {propertyName} LIKE '%", Enum.GetNames(typeof(TEnum)))}%'");

            return propertyBuilder;
        }
    }
}