using DiscontMarket.Domain.Models.Abstractions.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscontMarket.DAL.SqlServer.Configuration
{
    internal class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.HasKey(pa => new { pa.ProductID, pa.AttributeID });

            builder.HasOne(pa => pa.Product)
                .WithMany(p => p.ProductAttributes)
                .HasForeignKey(p => p.ProductID);

            builder.HasOne(pa => pa.Attribute)
                .WithMany(a => a.ProductAttributes)
                .HasForeignKey(p => p.AttributeID);
        }
    }
}
