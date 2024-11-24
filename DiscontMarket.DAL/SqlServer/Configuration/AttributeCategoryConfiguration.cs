using DiscontMarket.Domain.Models.Abstractions.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscontMarket.DAL.SqlServer.Configuration
{
    internal class AttributeCategoryConfiguration : IEntityTypeConfiguration<AttributeCategory>
    {
        public void Configure(EntityTypeBuilder<AttributeCategory> builder)
        {
            builder.HasKey(ac => new { ac.CategoryID, ac.AttributeID });

            builder.HasOne(ac => ac.Attribute)
                .WithMany(a => a.AttributeCategories)
                .HasForeignKey(ac => ac.AttributeID);

            builder.HasOne(ac => ac.Category)
                .WithMany(c => c.AttributeCategories)
                .HasForeignKey(ac => ac.CategoryID);    
        }
    }
}
