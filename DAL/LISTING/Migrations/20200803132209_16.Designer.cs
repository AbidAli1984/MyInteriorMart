﻿// <auto-generated />
using System;
using DAL.LISTING;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.LISTING.Migrations
{
    [DbContext(typeof(ListingDbContext))]
    [Migration("20200803132209_16")]
    partial class _16
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BOL.LISTING.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssemblyID")
                        .HasColumnType("int");

                    b.Property<int>("City")
                        .HasColumnType("int");

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<string>("LocalAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocalityID")
                        .HasColumnType("int");

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PincodeID")
                        .HasColumnType("int");

                    b.Property<int>("StateID")
                        .HasColumnType("int");

                    b.HasKey("AddressID");

                    b.ToTable("Address","listing");
                });

            modelBuilder.Entity("BOL.LISTING.AssembliesAndCities", b =>
                {
                    b.Property<int>("AssemblyAndCityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssemblyIDs")
                        .HasColumnType("int");

                    b.Property<int>("CityIDs")
                        .HasColumnType("int");

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateID")
                        .HasColumnType("int");

                    b.HasKey("AssemblyAndCityID");

                    b.ToTable("AssembliesAndCities","listing");
                });

            modelBuilder.Entity("BOL.LISTING.Branches", b =>
                {
                    b.Property<int>("BranchID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BranchAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BranchID");

                    b.ToTable("Branches","listing");
                });

            modelBuilder.Entity("BOL.LISTING.Categories", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FifthCategories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FirstCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("FourthCategories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SecondCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("SixthCategories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThirdCategories")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories","listing");
                });

            modelBuilder.Entity("BOL.LISTING.Certification", b =>
                {
                    b.Property<int>("CertificationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AcceptTenderWork")
                        .HasColumnType("bit");

                    b.Property<bool>("CompanyPanCard")
                        .HasColumnType("bit");

                    b.Property<bool>("GST")
                        .HasColumnType("bit");

                    b.Property<bool>("GomastaLicense")
                        .HasColumnType("bit");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ISOCertified")
                        .HasColumnType("bit");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ROCCertification")
                        .HasColumnType("bit");

                    b.HasKey("CertificationID");

                    b.ToTable("Certification","listing");
                });

            modelBuilder.Entity("BOL.LISTING.Communication", b =>
                {
                    b.Property<int>("CommunicationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Fax")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkypeID")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("TelephoneSecond")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("TollFree")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Whatsapp")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("CommunicationID");

                    b.ToTable("Communication","listing");
                });

            modelBuilder.Entity("BOL.LISTING.Listing", b =>
                {
                    b.Property<int>("ListingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NatureOfBusiness")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfEmployees")
                        .HasColumnType("int");

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Turnover")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("YearOfEstablishment")
                        .HasColumnType("datetime2");

                    b.HasKey("ListingID");

                    b.ToTable("Listing","listing");
                });

            modelBuilder.Entity("BOL.LISTING.PaymentMode", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Cash")
                        .HasColumnType("bit");

                    b.Property<bool>("Cheque")
                        .HasColumnType("bit");

                    b.Property<bool>("CreditCard")
                        .HasColumnType("bit");

                    b.Property<bool>("DebitCard")
                        .HasColumnType("bit");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<bool>("NetBanking")
                        .HasColumnType("bit");

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PayTM")
                        .HasColumnType("bit");

                    b.Property<bool>("Paypal")
                        .HasColumnType("bit");

                    b.Property<bool>("PhonePay")
                        .HasColumnType("bit");

                    b.Property<bool>("RtgsNeft")
                        .HasColumnType("bit");

                    b.HasKey("PaymentID");

                    b.ToTable("PaymentMode","listing");
                });

            modelBuilder.Entity("BOL.LISTING.Profile", b =>
                {
                    b.Property<int>("ProfileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.HasKey("ProfileID");

                    b.ToTable("Profile","listing");
                });

            modelBuilder.Entity("BOL.LISTING.SocialNetwork", b =>
                {
                    b.Property<int>("SocialNetworkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instagram")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Linkedin")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<string>("Others")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Others1")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pinterest")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Telegram")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("WhatsappGroupLink")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Youtube")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("SocialNetworkID");

                    b.ToTable("SocialNetwork","listing");
                });

            modelBuilder.Entity("BOL.LISTING.Specialisation", b =>
                {
                    b.Property<int>("SpecialisationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AcceptTenderWork")
                        .HasColumnType("bit");

                    b.Property<bool>("Banks")
                        .HasColumnType("bit");

                    b.Property<bool>("BeautyParlors")
                        .HasColumnType("bit");

                    b.Property<bool>("Bungalow")
                        .HasColumnType("bit");

                    b.Property<bool>("CallCenter")
                        .HasColumnType("bit");

                    b.Property<bool>("Church")
                        .HasColumnType("bit");

                    b.Property<bool>("Company")
                        .HasColumnType("bit");

                    b.Property<bool>("ComputerInstitute")
                        .HasColumnType("bit");

                    b.Property<bool>("Dispensary")
                        .HasColumnType("bit");

                    b.Property<bool>("ExhibitionStall")
                        .HasColumnType("bit");

                    b.Property<bool>("Factory")
                        .HasColumnType("bit");

                    b.Property<bool>("Farmhouse")
                        .HasColumnType("bit");

                    b.Property<bool>("Gurudwara")
                        .HasColumnType("bit");

                    b.Property<bool>("Gym")
                        .HasColumnType("bit");

                    b.Property<bool>("HealthClub")
                        .HasColumnType("bit");

                    b.Property<bool>("Home")
                        .HasColumnType("bit");

                    b.Property<bool>("Hospital")
                        .HasColumnType("bit");

                    b.Property<bool>("Hotel")
                        .HasColumnType("bit");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Laboratory")
                        .HasColumnType("bit");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<bool>("Mandir")
                        .HasColumnType("bit");

                    b.Property<bool>("Mosque")
                        .HasColumnType("bit");

                    b.Property<bool>("Office")
                        .HasColumnType("bit");

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Plazas")
                        .HasColumnType("bit");

                    b.Property<bool>("ResidentialSociety")
                        .HasColumnType("bit");

                    b.Property<bool>("Resorts")
                        .HasColumnType("bit");

                    b.Property<bool>("Restaurants")
                        .HasColumnType("bit");

                    b.Property<bool>("Salons")
                        .HasColumnType("bit");

                    b.Property<bool>("Shop")
                        .HasColumnType("bit");

                    b.Property<bool>("ShoppingMall")
                        .HasColumnType("bit");

                    b.Property<bool>("Showroom")
                        .HasColumnType("bit");

                    b.Property<bool>("Warehouse")
                        .HasColumnType("bit");

                    b.HasKey("SpecialisationID");

                    b.ToTable("Specialisation","listing");
                });

            modelBuilder.Entity("BOL.LISTING.WorkingHours", b =>
                {
                    b.Property<int>("WorkingHoursID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FridayFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FridayTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<DateTime>("MondayFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MondayTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SaturdayFrom")
                        .HasColumnType("datetime2");

                    b.Property<bool>("SaturdayHoliday")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SaturdayTo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SundayFrom")
                        .HasColumnType("datetime2");

                    b.Property<bool>("SundayHoliday")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SundayTo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ThursdayFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ThursdayTo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TuesdayFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TuesdayTo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WednesdayFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WednesdayTo")
                        .HasColumnType("datetime2");

                    b.HasKey("WorkingHoursID");

                    b.ToTable("WorkingHours","listing");
                });
#pragma warning restore 612, 618
        }
    }
}
