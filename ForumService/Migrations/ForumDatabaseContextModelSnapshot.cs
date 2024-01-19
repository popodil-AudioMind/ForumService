﻿// <auto-generated />
using System;
using ForumService.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ForumService.Migrations
{
    [DbContext(typeof(ForumDatabaseContext))]
    partial class ForumDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ForumService.Interfaces.IForum", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("updateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("uploadDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("userId")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.ToTable("Forums");
                });
#pragma warning restore 612, 618
        }
    }
}