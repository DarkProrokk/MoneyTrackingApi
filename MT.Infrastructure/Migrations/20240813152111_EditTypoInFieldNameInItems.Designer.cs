﻿// <auto-generated />
using System;
using MT.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MT.Infrastructure.Migrations
{
    [DbContext(typeof(TrackingContext))]
    [Migration("20240813152111_EditTypoInFieldNameInItems")]
    partial class EditTypoInFieldNameInItems
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ItemTag", b =>
                {
                    b.Property<Guid>("ItemsItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagsTagId")
                        .HasColumnType("uuid");

                    b.HasKey("ItemsItemId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("ItemTag");
                });

            modelBuilder.Entity("MT.Domain.Entity.Item", b =>
                {
                    b.Property<Guid>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasMaxLength(6)
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<decimal?>("PossiblePrice")
                        .HasColumnType("numeric");

                    b.Property<bool?>("PossibleUseful")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<bool>("Useful")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("ItemId");

                    b.HasIndex(new[] { "UserId" }, "IX_Items_UserId");

                    b.ToTable("items");
                });

            modelBuilder.Entity("MT.Domain.Entity.Tag", b =>
                {
                    b.Property<Guid>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("TagId");

                    b.HasIndex("UserId");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("MT.Domain.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("MT.Domain.Entity.Userclaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("userclaims");
                });

            modelBuilder.Entity("ItemTag", b =>
                {
                    b.HasOne("MT.Domain.Entity.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemsItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MT.Domain.Entity.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MT.Domain.Entity.Item", b =>
                {
                    b.HasOne("MT.Domain.Entity.User", "User")
                        .WithMany("Items")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Items_Users_UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MT.Domain.Entity.Tag", b =>
                {
                    b.HasOne("MT.Domain.Entity.User", "User")
                        .WithMany("Tags")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MT.Domain.Entity.User", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
