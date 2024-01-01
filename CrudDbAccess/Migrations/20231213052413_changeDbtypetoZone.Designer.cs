﻿// <auto-generated />
using System;
using CrudDbAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CrudDbAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231213052413_changeDbtypetoZone")]
    partial class changeDbtypetoZone
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CrudDbAccess.Data.BaseAccessData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<Guid>("FromId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<Guid>("ToId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.ToTable("AccessInformation");
                });

            modelBuilder.Entity("CrudDbAccess.Data.DatabaseDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Zone")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("InfraDatabases");
                });

            modelBuilder.Entity("CrudDbAccess.DataModels.BaseInfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("from")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("port")
                        .HasColumnType("int");

                    b.Property<string>("to")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("BaseInfo");
                });

            modelBuilder.Entity("CrudDbAccess.Data.BaseAccessData", b =>
                {
                    b.HasOne("CrudDbAccess.Data.DatabaseDetails", "From")
                        .WithMany()
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("CrudDbAccess.Data.DatabaseDetails", "To")
                        .WithMany()
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("To");
                });
#pragma warning restore 612, 618
        }
    }
}