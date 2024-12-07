using DiscontMarket.Domain.Models.Abstractions.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscontMarket.DAL.SqlServer.Configuration
{
    internal class BrandCategoryConfiguration : IEntityTypeConfiguration<BrandCategory>
    {
        public void Configure(EntityTypeBuilder<BrandCategory> builder)
        {
            builder.HasKey(bc => new { bc.BrandID, bc.CategoryID });

            builder.HasOne(bc => bc.Brand)
                .WithMany(b => b.BrandCategories)
                .HasForeignKey(bc => bc.BrandID)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(bc => bc.Category)
                .WithMany(c => c.BrandCategories)
                .HasForeignKey(bc => bc.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
