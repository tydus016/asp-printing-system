﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using printingsystem.Data;

#nullable disable

namespace printingsystem.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230608133440_5th")]
    partial class _5th
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("printingsystem.Models.Files.Files", b =>
                {
                    b.Property<Guid>("file_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("copies")
                        .HasColumnType("int");

                    b.Property<string>("filename")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("fk_user_id")
                        .HasColumnType("int");

                    b.Property<string>("paper_type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("printer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("file_id");

                    b.ToTable("files");
                });

            modelBuilder.Entity("printingsystem.Models.Prints.Prints", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("files")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("no_of_copies")
                        .HasColumnType("int");

                    b.Property<int>("no_of_pages")
                        .HasColumnType("int");

                    b.Property<string>("notes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("printer_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("type_of_paper")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("prints");
                });

            modelBuilder.Entity("printingsystem.Models.Users.Users", b =>
                {
                    b.Property<Guid>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("fullname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("id_number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("user_type")
                        .HasColumnType("int");

                    b.HasKey("user_id");

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}