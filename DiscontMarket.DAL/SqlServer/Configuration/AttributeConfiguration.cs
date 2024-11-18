using DiscontMarket.DAL.SqlServer.Helpers;
using DiscontMarket.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DiscontMarket.DAL.SqlServer.Configuration
{
    public class AttributeConfiguration : IEntityTypeConfiguration<AttributeEntity>
    {
        public void Configure(EntityTypeBuilder<AttributeEntity> builder)
        {
            builder.HasKey(a => a.ID);

            PropertyHelper<AttributeEntity>.SetProperties(builder, false,
                a => a.AttributeName
            );

        }
    }

}