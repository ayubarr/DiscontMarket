using DiscontMarket.DAL.SqlServer.Helpers;
using DiscontMarket.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscontMarket.DAL.SqlServer.Configuration
{
    internal class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(b => b.ID);

            PropertyHelper<Brand>.SetProperties(builder, false,
                b => b.Name
            );

            // Описание связи для сушностей Brend и Product
            builder.HasOne(b => b.Product)
                .WithOne(s => s.Brand)
                .HasForeignKey<Brand>(b => b.ProductID);
        }
    }
}