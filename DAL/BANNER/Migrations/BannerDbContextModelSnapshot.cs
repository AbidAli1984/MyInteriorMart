﻿// <auto-generated />
using System;
using DAL.BANNER;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.BANNER.Migrations
{
    [DbContext(typeof(BannerDbContext))]
    partial class BannerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BOL.BANNER.BannerPage", b =>
                {
                    b.Property<int>("BannerPageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BannerPageID");

                    b.ToTable("BannerPage","banner");
                });

            modelBuilder.Entity("BOL.BANNER.BannerSpace", b =>
                {
                    b.Property<int>("BannerSpaceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BannerPageID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Height")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpaceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Width")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BannerSpaceID");

                    b.HasIndex("BannerPageID");

                    b.ToTable("BannerSpace","banner");
                });

            modelBuilder.Entity("BOL.BANNER.BannerType", b =>
                {
                    b.Property<int>("BannerTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BannerTypeID");

                    b.ToTable("BannerType","banner");
                });

            modelBuilder.Entity("BOL.BANNER.Campaign", b =>
                {
                    b.Property<int>("CampaignID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BannerPageID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("BannerSpaceID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("BannerTypeID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("CampaignName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClicksCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("DestinationURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("HTML5BannerAdScript")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageAltText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ImpressionsCount")
                        .HasColumnType("int");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<string>("OwnerGUID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("VideoURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CampaignID");

                    b.HasIndex("BannerPageID");

                    b.HasIndex("BannerSpaceID");

                    b.HasIndex("BannerTypeID");

                    b.ToTable("Campaign","banner");
                });

            modelBuilder.Entity("BOL.BANNER.Slideshow", b =>
                {
                    b.Property<int>("SlideID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AltAttribute")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("TargetURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.HasKey("SlideID");

                    b.ToTable("Slideshow");
                });

            modelBuilder.Entity("BOL.BANNER.BannerSpace", b =>
                {
                    b.HasOne("BOL.BANNER.BannerPage", "BannerPage")
                        .WithMany("BannerSpace")
                        .HasForeignKey("BannerPageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BOL.BANNER.Campaign", b =>
                {
                    b.HasOne("BOL.BANNER.BannerPage", "BannerPage")
                        .WithMany("Campaign")
                        .HasForeignKey("BannerPageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BOL.BANNER.BannerSpace", "BannerSpace")
                        .WithMany("Campaign")
                        .HasForeignKey("BannerSpaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BOL.BANNER.BannerType", "BannerType")
                        .WithMany("Campaign")
                        .HasForeignKey("BannerTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
