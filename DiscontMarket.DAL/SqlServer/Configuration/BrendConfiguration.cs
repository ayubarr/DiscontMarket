using DiscontMarket.DAL.SqlServer.Helpers;
using DiscontMarket.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DiscontMarket.DAL.SqlServer.Configuration
{
    public class BrendConfiguration : IEntityTypeConfiguration<Brend>
    {
        public void Configure(EntityTypeBuilder<Brend> builder)
        {
            builder.HasKey(b => b.ID);

            PropertyHelper<Brend>.SetProperties(builder, false,
                b => b.BrendName
            );

            PropertyHelper<Brend>.SetProperties(builder, true,
                b => b.Description
            );

            // Описание связи для сушностей Brend и Product
            builder.HasOne(b => b.Product)
                .WithMany(s => s.Brends)
                .HasForeignKey(b => b.ProductID);
        }
    }
}