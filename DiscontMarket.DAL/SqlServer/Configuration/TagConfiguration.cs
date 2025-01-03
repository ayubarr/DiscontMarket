﻿using DiscontMarket.DAL.SqlServer.Helpers;
using DiscontMarket.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscontMarket.DAL.SqlServer.Configuration
{
    internal class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.ID);

            PropertyHelper<Tag>.SetProperties(builder, false,
                t => t.TagName
            );

        }
    }
}