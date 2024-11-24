using DiscontMarket.DAL.SqlServer.Helpers;
using DiscontMarket.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DiscontMarket.DAL.SqlServer.Configuration
{
    internal class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.ID);

            PropertyHelper<Image>.SetProperties(builder, true,
                i => i.Path
            );

            //Описание связи сущностей Image и Product
            builder.HasOne(i => i.Product)
               .WithMany(p => p.Images)
               .HasForeignKey(i => i.ProductID);
        }
    }
}
