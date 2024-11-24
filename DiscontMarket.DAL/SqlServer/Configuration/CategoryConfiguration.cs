using DiscontMarket.DAL.SqlServer.Helpers;
using DiscontMarket.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DiscontMarket.DAL.SqlServer.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.ID);

            PropertyHelper<Category>.SetProperties(builder, false,
                o => o.Name
                );


            //Описание связи сущностей Category и Product
            builder.HasOne(c => c.Product)
               .WithMany(p => p.Categories)
               .HasForeignKey(c => c.ProductID);
        }
    }
}