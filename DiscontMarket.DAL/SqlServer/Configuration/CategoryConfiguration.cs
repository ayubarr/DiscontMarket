using DiscontMarket.DAL.SqlServer.Helpers;
using DiscontMarket.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace DiscontMarket.DAL.SqlServer.Configuration
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
            builder.HasKey(c => c.ID);

            PropertyHelper<Category>.SetProperties(builder, false,
                o => o.Name,
                c => c.Type
            );

            //Описание связи сущностей Category и User
            builder.HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserID);

            //Описание связи сущностей Category и Product
            builder.HasOne(c => c.Product)
               .WithMany(p => p.Categories)
               .HasForeignKey(c => c.ProductID);
        }
	}
}