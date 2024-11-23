﻿using DiscontMarket.DAL.SqlServer.Helpers;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscontMarket.DAL.SqlServer.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ID);

            PropertyHelper<Product>.SetProperties(builder, false,
               p => p.ProductName
            );

            PropertyHelper<Product>.SetProperties(builder,
                p => p.Price
            );

            builder.Property(p => p.ProductAvailability)
                .HasConversion(
                     v => v.ToString(), // Сохранение как строка
                     v => (Availability)Enum.Parse(typeof(Availability), v)
                );

            builder.Property(p => p.ProductStatus)
                .HasConversion(
                    v => v.ToString(), // Сохранение как строка
                    v => (ProductStatus)Enum.Parse(typeof(ProductStatus), v)
                );

            builder.HasOne(p => p.User)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UserID);

            builder.HasOne(p => p.Brend)
                .WithOne(b => b.Product)
                .HasForeignKey<Product>(p => p.BrendId);
            
        }
    }
}
