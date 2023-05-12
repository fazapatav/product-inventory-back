﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sofka.ProductInventory.Infrastucture.Data;

#nullable disable

namespace Sofka.ProductInventory.Infrastucture.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230512201532_InitialDb")]
    partial class InitialDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sofka.ProductInventory.Core.Entities.Buy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Buy");
                });

            modelBuilder.Entity("Sofka.ProductInventory.Core.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BuyId")
                        .HasColumnType("int");

                    b.Property<int>("InInventory")
                        .HasColumnType("int");

                    b.Property<int>("Max")
                        .HasColumnType("int");

                    b.Property<int>("Min")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuyId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Sofka.ProductInventory.Core.Entities.Product", b =>
                {
                    b.HasOne("Sofka.ProductInventory.Core.Entities.Buy", null)
                        .WithMany("Products")
                        .HasForeignKey("BuyId");
                });

            modelBuilder.Entity("Sofka.ProductInventory.Core.Entities.Buy", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}