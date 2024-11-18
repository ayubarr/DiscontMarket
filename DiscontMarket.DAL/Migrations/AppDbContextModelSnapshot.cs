﻿// <auto-generated />
using System;
using DiscontMarket.DAL.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiscontMarket.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DiscontMarket.Domain.Models.Abstractions.BaseEntities.ApplicationRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Abstractions.BaseEntities.ApplicationUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("ApplicationUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Abstractions.LinkEntities.ProductAttribute", b =>
                {
                    b.Property<long>("ProductID")
                        .HasColumnType("bigint");

                    b.Property<long>("AttributeID")
                        .HasColumnType("bigint");

                    b.HasKey("ProductID", "AttributeID");

                    b.HasIndex("AttributeID");

                    b.ToTable("ProductAttribute");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Abstractions.LinkEntities.ProductTag", b =>
                {
                    b.Property<long>("ProductID")
                        .HasColumnType("bigint");

                    b.Property<long>("TagID")
                        .HasColumnType("bigint");

                    b.HasKey("ProductID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("ProductTag");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.AttributeEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("AttributeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Brend", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("BrendName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.Property<long?>("ProductID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.ToTable("Brends");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Category", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<long?>("ProductID")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.HasIndex("UserID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Order", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<int>("Condition")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp(2)");

                    b.Property<long?>("ProductID")
                        .HasColumnType("bigint");

                    b.Property<long?>("SessionID")
                        .HasColumnType("bigint");

                    b.Property<decimal>("TotalCost")
                        .HasPrecision(15, 2)
                        .HasColumnType("numeric");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.HasIndex("SessionID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Product", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<bool>("AvailStatus")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Cost")
                        .HasPrecision(15, 2)
                        .HasColumnType("numeric");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<long?>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Session", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("InitialTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ID");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Tag", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<uint>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<uint>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<uint>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<uint>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<uint>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.User", b =>
                {
                    b.HasBaseType("DiscontMarket.Domain.Models.Abstractions.BaseEntities.ApplicationUser");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Abstractions.LinkEntities.ProductAttribute", b =>
                {
                    b.HasOne("DiscontMarket.Domain.Models.Entities.AttributeEntity", "Attribute")
                        .WithMany("ProductAttributes")
                        .HasForeignKey("AttributeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscontMarket.Domain.Models.Entities.Product", "Product")
                        .WithMany("ProductAttributes")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Abstractions.LinkEntities.ProductTag", b =>
                {
                    b.HasOne("DiscontMarket.Domain.Models.Entities.Product", "Product")
                        .WithMany("ProductTags")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscontMarket.Domain.Models.Entities.Tag", "Tag")
                        .WithMany("ProductTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Brend", b =>
                {
                    b.HasOne("DiscontMarket.Domain.Models.Entities.Product", "Product")
                        .WithMany("Brends")
                        .HasForeignKey("ProductID");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Category", b =>
                {
                    b.HasOne("DiscontMarket.Domain.Models.Entities.Product", "Product")
                        .WithMany("Categories")
                        .HasForeignKey("ProductID");

                    b.HasOne("DiscontMarket.Domain.Models.Entities.User", "User")
                        .WithMany("Categories")
                        .HasForeignKey("UserID");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Order", b =>
                {
                    b.HasOne("DiscontMarket.Domain.Models.Entities.Product", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("ProductID");

                    b.HasOne("DiscontMarket.Domain.Models.Entities.Session", "Session")
                        .WithMany("Orders")
                        .HasForeignKey("SessionID");

                    b.Navigation("Product");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Product", b =>
                {
                    b.HasOne("DiscontMarket.Domain.Models.Entities.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<uint>", b =>
                {
                    b.HasOne("DiscontMarket.Domain.Models.Abstractions.BaseEntities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<uint>", b =>
                {
                    b.HasOne("DiscontMarket.Domain.Models.Abstractions.BaseEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<uint>", b =>
                {
                    b.HasOne("DiscontMarket.Domain.Models.Abstractions.BaseEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<uint>", b =>
                {
                    b.HasOne("DiscontMarket.Domain.Models.Abstractions.BaseEntities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscontMarket.Domain.Models.Abstractions.BaseEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<uint>", b =>
                {
                    b.HasOne("DiscontMarket.Domain.Models.Abstractions.BaseEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.AttributeEntity", b =>
                {
                    b.Navigation("ProductAttributes");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Product", b =>
                {
                    b.Navigation("Brends");

                    b.Navigation("Categories");

                    b.Navigation("Orders");

                    b.Navigation("ProductAttributes");

                    b.Navigation("ProductTags");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Session", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.Tag", b =>
                {
                    b.Navigation("ProductTags");
                });

            modelBuilder.Entity("DiscontMarket.Domain.Models.Entities.User", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
