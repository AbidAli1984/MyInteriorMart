using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.LISTING.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "listing");

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "listing",
                columns: table => new
                {
                    AddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    CountryID = table.Column<int>(nullable: false),
                    StateID = table.Column<int>(nullable: false),
                    City = table.Column<int>(nullable: false),
                    AssemblyID = table.Column<int>(nullable: false),
                    PincodeID = table.Column<int>(nullable: false),
                    LocalityID = table.Column<int>(nullable: false),
                    LocalAddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "AssembliesAndCities",
                schema: "listing",
                columns: table => new
                {
                    AssemblyAndCityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryID = table.Column<int>(nullable: false),
                    StateID = table.Column<int>(nullable: false),
                    CityIDs = table.Column<int>(nullable: false),
                    AssemblyIDs = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssembliesAndCities", x => x.AssemblyAndCityID);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                schema: "listing",
                columns: table => new
                {
                    BranchID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    BranchName = table.Column<string>(maxLength: 200, nullable: false),
                    ContactPerson = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Mobile = table.Column<string>(nullable: false),
                    Telephone = table.Column<string>(nullable: true),
                    BranchAddress = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "listing",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    SecondCategoryID = table.Column<int>(nullable: false),
                    ThirdCategories = table.Column<string>(nullable: true),
                    FourthCategories = table.Column<string>(nullable: true),
                    FifthCategories = table.Column<string>(nullable: true),
                    SixthCategories = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Certification",
                schema: "listing",
                columns: table => new
                {
                    CertificationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    GST = table.Column<bool>(nullable: false),
                    ISOCertified = table.Column<bool>(nullable: false),
                    CompanyPanCard = table.Column<bool>(nullable: false),
                    ROCCertification = table.Column<bool>(nullable: false),
                    GomastaLicense = table.Column<bool>(nullable: false),
                    AcceptTenderWork = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certification", x => x.CertificationID);
                });

            migrationBuilder.CreateTable(
                name: "Communication",
                schema: "listing",
                columns: table => new
                {
                    CommunicationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Website = table.Column<string>(maxLength: 1000, nullable: true),
                    Mobile = table.Column<string>(maxLength: 15, nullable: false),
                    Whatsapp = table.Column<string>(maxLength: 15, nullable: false),
                    Telephone = table.Column<string>(maxLength: 15, nullable: true),
                    TollFree = table.Column<string>(maxLength: 30, nullable: true),
                    Fax = table.Column<string>(maxLength: 15, nullable: true),
                    SkypeID = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communication", x => x.CommunicationID);
                });

            migrationBuilder.CreateTable(
                name: "Listing",
                schema: "listing",
                columns: table => new
                {
                    ListingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<string>(maxLength: 20, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 200, nullable: false),
                    YearOfEstablishment = table.Column<DateTime>(nullable: false),
                    NumberOfEmployees = table.Column<int>(nullable: false),
                    Designation = table.Column<string>(nullable: false),
                    NatureOfBusiness = table.Column<string>(nullable: false),
                    Turnover = table.Column<string>(nullable: false),
                    ListingURL = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listing", x => x.ListingID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMode",
                schema: "listing",
                columns: table => new
                {
                    PaymentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    Cash = table.Column<bool>(nullable: false),
                    NetBanking = table.Column<bool>(nullable: false),
                    Cheque = table.Column<bool>(nullable: false),
                    RtgsNeft = table.Column<bool>(nullable: false),
                    DebitCard = table.Column<bool>(nullable: false),
                    CreditCard = table.Column<bool>(nullable: false),
                    PayTM = table.Column<bool>(nullable: false),
                    PhonePay = table.Column<bool>(nullable: false),
                    Paypal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMode", x => x.PaymentID);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                schema: "listing",
                columns: table => new
                {
                    ProfileID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    ProfileDetails = table.Column<string>(maxLength: 10000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.ProfileID);
                });

            migrationBuilder.CreateTable(
                name: "SocialNetwork",
                schema: "listing",
                columns: table => new
                {
                    SocialNetworkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    WhatsappGroupLink = table.Column<string>(maxLength: 500, nullable: true),
                    Youtube = table.Column<string>(maxLength: 500, nullable: true),
                    Facebook = table.Column<string>(maxLength: 500, nullable: true),
                    Instagram = table.Column<string>(maxLength: 500, nullable: true),
                    Linkedin = table.Column<string>(maxLength: 500, nullable: true),
                    Pinterest = table.Column<string>(maxLength: 500, nullable: true),
                    Telegram = table.Column<string>(maxLength: 500, nullable: true),
                    Others = table.Column<string>(maxLength: 500, nullable: true),
                    Others1 = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialNetwork", x => x.SocialNetworkID);
                });

            migrationBuilder.CreateTable(
                name: "Specialisation",
                schema: "listing",
                columns: table => new
                {
                    SpecialisationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    Banks = table.Column<bool>(nullable: false),
                    BeautyParlors = table.Column<bool>(nullable: false),
                    Bungalow = table.Column<bool>(nullable: false),
                    CallCenter = table.Column<bool>(nullable: false),
                    Church = table.Column<bool>(nullable: false),
                    Company = table.Column<bool>(nullable: false),
                    ComputerInstitute = table.Column<bool>(nullable: false),
                    Dispensary = table.Column<bool>(nullable: false),
                    ExhibitionStall = table.Column<bool>(nullable: false),
                    Factory = table.Column<bool>(nullable: false),
                    Farmhouse = table.Column<bool>(nullable: false),
                    Gurudwara = table.Column<bool>(nullable: false),
                    Gym = table.Column<bool>(nullable: false),
                    HealthClub = table.Column<bool>(nullable: false),
                    Home = table.Column<bool>(nullable: false),
                    Hospital = table.Column<bool>(nullable: false),
                    Hotel = table.Column<bool>(nullable: false),
                    Laboratory = table.Column<bool>(nullable: false),
                    Mandir = table.Column<bool>(nullable: false),
                    Mosque = table.Column<bool>(nullable: false),
                    Office = table.Column<bool>(nullable: false),
                    Plazas = table.Column<bool>(nullable: false),
                    ResidentialSociety = table.Column<bool>(nullable: false),
                    Resorts = table.Column<bool>(nullable: false),
                    Restaurants = table.Column<bool>(nullable: false),
                    Salons = table.Column<bool>(nullable: false),
                    Shop = table.Column<bool>(nullable: false),
                    ShoppingMall = table.Column<bool>(nullable: false),
                    Showroom = table.Column<bool>(nullable: false),
                    Warehouse = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialisation", x => x.SpecialisationID);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                schema: "listing",
                columns: table => new
                {
                    WorkingHoursID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    MondayFrom = table.Column<DateTime>(nullable: false),
                    MondayTo = table.Column<string>(nullable: false),
                    TuesdayFrom = table.Column<DateTime>(nullable: false),
                    TuesdayTo = table.Column<DateTime>(nullable: false),
                    WednesdayFrom = table.Column<DateTime>(nullable: false),
                    WednesdayTo = table.Column<DateTime>(nullable: false),
                    ThursdayFrom = table.Column<DateTime>(nullable: false),
                    ThursdayTo = table.Column<DateTime>(nullable: false),
                    FridayFrom = table.Column<DateTime>(nullable: false),
                    FridayTo = table.Column<DateTime>(nullable: false),
                    SaturdayHoliday = table.Column<bool>(nullable: false),
                    SaturdayFrom = table.Column<DateTime>(nullable: false),
                    SaturdayTo = table.Column<DateTime>(nullable: false),
                    SundayHoliday = table.Column<bool>(nullable: false),
                    SundayFrom = table.Column<DateTime>(nullable: false),
                    SundayTo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.WorkingHoursID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "listing");

            migrationBuilder.DropTable(
                name: "AssembliesAndCities",
                schema: "listing");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "listing");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "listing");

            migrationBuilder.DropTable(
                name: "Certification",
                schema: "listing");

            migrationBuilder.DropTable(
                name: "Communication",
                schema: "listing");

            migrationBuilder.DropTable(
                name: "Listing",
                schema: "listing");

            migrationBuilder.DropTable(
                name: "PaymentMode",
                schema: "listing");

            migrationBuilder.DropTable(
                name: "Profile",
                schema: "listing");

            migrationBuilder.DropTable(
                name: "SocialNetwork",
                schema: "listing");

            migrationBuilder.DropTable(
                name: "Specialisation",
                schema: "listing");

            migrationBuilder.DropTable(
                name: "WorkingHours",
                schema: "listing");
        }
    }
}
