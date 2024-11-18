using DiscontMarket.DAL.SqlServer.Helpers;
using DiscontMarket.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscontMarket.DAL.SqlServer.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.ID);

            PropertyHelper<Order>.SetProperties(builder,
                o => o.TotalCost
            );
     
            PropertyHelper<Order>.SetProperties(builder,
                o => o.CreationDate
            );

            //Описание связи для сушностей Session и Order
            builder.HasOne(o => o.Session)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.SessionID);

            //Описание связи для сушностей Product и Order
            builder.HasOne(o => o.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.ProductID);
        }
    }
}
