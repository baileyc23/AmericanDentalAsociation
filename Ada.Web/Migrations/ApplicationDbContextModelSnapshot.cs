﻿// <auto-generated />
using Ada.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ada.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ada.Web.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Azure"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "AWS"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Sitefinity"
                        },
                        new
                        {
                            Id = 4,
                            DisplayOrder = 4,
                            Name = "Generative AI"
                        },
                        new
                        {
                            Id = 5,
                            DisplayOrder = 5,
                            Name = "Terraform"
                        },
                        new
                        {
                            Id = 6,
                            DisplayOrder = 6,
                            Name = "Databricks"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
