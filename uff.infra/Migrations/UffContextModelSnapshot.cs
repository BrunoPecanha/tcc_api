﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using uff.Infra.Context;

#nullable disable

namespace uff.infra.Migrations
{
    [DbContext(typeof(UffContext))]
    partial class UffContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("uff.Domain.Entity.Costumer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("Active");

                    b.Property<string>("City")
                        .HasColumnType("text")
                        .HasColumnName("City");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("LastName");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("LastUpdate");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.Property<string>("Number")
                        .HasColumnType("text")
                        .HasColumnName("Number");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("Phone");

                    b.Property<DateTime>("RegisteringDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("RegisteringDate");

                    b.Property<string>("Street")
                        .HasColumnType("text")
                        .HasColumnName("Street");

                    b.HasKey("Id");

                    b.ToTable("Costumer", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
