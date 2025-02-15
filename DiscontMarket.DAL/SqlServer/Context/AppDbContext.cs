﻿using DiscontMarket.DAL.SqlServer.Configuration;
using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiscontMarket.DAL.SqlServer.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, uint>
    {
        public AppDbContext() : base()
        {
            Database.EnsureCreated();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AttributeEntity> Attributes { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(AttributeCategoryConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(AttributeConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(BrandConfiguration).Assembly)               
                .ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(ImageConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(ProductAttributeConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(ProductTagConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(SessionConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(TagConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);



        }
    }
}
