﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QwertyAPI.Models;

#nullable disable

namespace QwertyApi.Migrations
{
    [DbContext(typeof(QwertyDbContext))]
    [Migration("20220131225549_AddQwertyFavColor")]
    partial class AddQwertyFavColor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QwertyAPI.Models.QwertyFavColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QwertyProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("QwertyProfileId")
                        .IsUnique();

                    b.ToTable("QwertyFavColors");
                });

            modelBuilder.Entity("QwertyAPI.Models.QwertyMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("QwertyProfileId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("QwertyProfileId");

                    b.ToTable("QwertyMessages");
                });

            modelBuilder.Entity("QwertyAPI.Models.QwertyProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("QwertyProfiles");
                });

            modelBuilder.Entity("QwertyAPI.Models.QwertyFavColor", b =>
                {
                    b.HasOne("QwertyAPI.Models.QwertyProfile", "QwertyProfile")
                        .WithOne("FavColor")
                        .HasForeignKey("QwertyAPI.Models.QwertyFavColor", "QwertyProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QwertyProfile");
                });

            modelBuilder.Entity("QwertyAPI.Models.QwertyMessage", b =>
                {
                    b.HasOne("QwertyAPI.Models.QwertyProfile", "QwertyProfile")
                        .WithMany("Messages")
                        .HasForeignKey("QwertyProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QwertyProfile");
                });

            modelBuilder.Entity("QwertyAPI.Models.QwertyProfile", b =>
                {
                    b.Navigation("FavColor");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}