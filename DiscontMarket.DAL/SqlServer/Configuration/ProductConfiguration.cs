using DiscontMarket.DAL.SqlServer.Helpers;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscontMarket.DAL.SqlServer.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ID);

            PropertyHelper<Product>.SetProperties(builder, false,
               p => p.ProductName,
               p => p.IconPath
            );

            PropertyHelper<Product>.SetProperties(builder, true,
              p => p.Description,
              p => p.FullDescription
            );

            PropertyHelper<Product>.SetProperties(builder,
              p => p.Price
            );
            PropertyHelper<Product>.SetProperties(builder,
              p => p.Rating
            );

            builder.Property(p => p.Availability)
                .HasConversion(
                     v => v.ToString(), // Сохранение как строка
                     v => (Availability)Enum.Parse(typeof(Availability), v)
                );

            builder.Property(p => p.Status)
                .HasConversion(
                    v => v.ToString(), // Сохранение как строка
                    v => (ProductStatus)Enum.Parse(typeof(ProductStatus), v)
                );

            builder.HasOne(p => p.User)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UserID);

            builder.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandID);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID);
            
        }
    }
}
