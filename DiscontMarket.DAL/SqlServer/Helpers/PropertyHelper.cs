using DiscontMarket.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace DiscontMarket.DAL.SqlServer.Helpers
{
    internal static class PropertyHelper<T>
        where T : class
    {
        internal static void SetProperties(EntityTypeBuilder<T> builder, bool isMaxLength = false,
            params Expression<Func<T, string>>[] properties)
        {
            foreach (var property in properties)
            {
                builder.Property(property)
                    .HasMaxLength(isMaxLength ? DefaultConstraints.DefaultMaxLength : DefaultConstraints.DefaultLength)
                    .HasColumnType(PgSqlColumnTypes.Varchar);
            }
        }
      

        internal static void SetProperties(EntityTypeBuilder<T> builder,
            params Expression<Func<T, decimal>>[] properties)
        {
            foreach (var property in properties)
            {
                builder.Property(property)
                    .HasPrecision(DefaultConstraints.DefaultMaxPrecision, DefaultConstraints.DefaultMaxPrecisionDecimalPoint)
                    .HasColumnType(PgSqlColumnTypes.Numeric);
            }
        }

        internal static void SetProperties(EntityTypeBuilder<T> builder,
            params Expression<Func<T, bool>>[] properties)
        {
            foreach (var property in properties)
            {
                builder.Property(property)
                    .HasColumnType(PgSqlColumnTypes.Boolean);
            }
        }

        internal static void SetProperties(EntityTypeBuilder<T> builder,
            params Expression<Func<T, int>>[] properties)
        {
            foreach (var property in properties)
            {
                builder.Property(property)
                    .HasColumnType(PgSqlColumnTypes.Integer);
            }
        }

        internal static void SetProperties(EntityTypeBuilder<T> builder,
            params Expression<Func<T, DateTime>>[] properties)
        {
            foreach (var property in properties)
            {
                builder.Property(property)
                    .HasColumnType($"{PgSqlColumnTypes.Timestamp}({DefaultConstraints.DefaultMaxPrecisionDecimalPoint})");
            }
        }
    }
}
