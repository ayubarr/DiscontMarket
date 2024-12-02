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
                    .HasColumnType(PgSqlColumnType.Varchar);
            }
        }
      

        internal static void SetProperties(EntityTypeBuilder<T> builder,
            params Expression<Func<T, decimal>>[] properties)
        {
            foreach (var property in properties)
            {
                builder.Property(property)
                    .HasPrecision(DefaultConstraints.DefaultMaxPrecision, DefaultConstraints.DefaultMaxPrecisionDecimalPoint)
                    .HasColumnType(PgSqlColumnType.Numeric);
            }
        }

        internal static void SetProperties(EntityTypeBuilder<T> builder,
            params Expression<Func<T, bool>>[] properties)
        {
            foreach (var property in properties)
            {
                builder.Property(property)
                    .HasColumnType(PgSqlColumnType.Boolean);
            }
        }

        internal static void SetProperties(EntityTypeBuilder<T> builder,
            params Expression<Func<T, int>>[] properties)
        {
            foreach (var property in properties)
            {
                builder.Property(property)
                    .HasColumnType(PgSqlColumnType.Integer);
            }
        }

        internal static void SetProperties(EntityTypeBuilder<T> builder,
           params Expression<Func<T, double>>[] properties)
        {
            foreach (var property in properties)
            {
                builder.Property(property)
                    .HasColumnType(PgSqlColumnType.DoublePrecision);
            }
        }

        internal static void SetProperties(EntityTypeBuilder<T> builder,
            params Expression<Func<T, DateTime>>[] properties)
        {
            foreach (var property in properties)
            {
                builder.Property(property)
                    .HasColumnType($"{PgSqlColumnType.Timestamp}({DefaultConstraints.DefaultMaxPrecisionDecimalPoint})");
            }
        }
    }
}
