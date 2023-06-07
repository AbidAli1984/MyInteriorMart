using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.BILLING.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "plan");

            migrationBuilder.CreateTable(
                name: "AdvertisementPlan",
                schema: "plan",
                columns: table => new
                {
                    PlanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementPlan", x => x.PlanID);
                });

            migrationBuilder.CreateTable(
                name: "BannerPlan",
                schema: "plan",
                columns: table => new
                {
                    PlanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    MonthlyPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerPlan", x => x.PlanID);
                });

            migrationBuilder.CreateTable(
                name: "DataPlan",
                schema: "plan",
                columns: table => new
                {
                    PlanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPlan", x => x.PlanID);
                });

            migrationBuilder.CreateTable(
                name: "EmailPlans",
                schema: "plan",
                columns: table => new
                {
                    PlanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailPlans", x => x.PlanID);
                });

            migrationBuilder.CreateTable(
                name: "MagazinePlan",
                schema: "plan",
                columns: table => new
                {
                    PlanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagazinePlan", x => x.PlanID);
                });

            migrationBuilder.CreateTable(
                name: "Period",
                schema: "plan",
                columns: table => new
                {
                    PeriodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    DurationInMonths = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Period", x => x.PeriodID);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                schema: "plan",
                columns: table => new
                {
                    PlanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    MonthlyPrice = table.Column<int>(nullable: false),
                    Categories = table.Column<int>(nullable: false),
                    Offers = table.Column<int>(nullable: false),
                    Products = table.Column<int>(nullable: false),
                    JobPostings = table.Column<int>(nullable: false),
                    RecentProjects = table.Column<int>(nullable: false),
                    City = table.Column<int>(nullable: false),
                    PriorityListing = table.Column<bool>(nullable: false),
                    SmsNotifications = table.Column<bool>(nullable: false),
                    EmailNotifications = table.Column<bool>(nullable: false),
                    UserHistory = table.Column<bool>(nullable: false),
                    Analytics = table.Column<bool>(nullable: false),
                    MembershipBadge = table.Column<bool>(nullable: false),
                    PhotoGallery = table.Column<bool>(nullable: false),
                    MultipleLocations = table.Column<bool>(nullable: false),
                    OneHourService = table.Column<bool>(nullable: false),
                    Team = table.Column<bool>(nullable: false),
                    MonopolyProducts = table.Column<bool>(nullable: false),
                    Branches = table.Column<bool>(nullable: false),
                    ClientLogo = table.Column<bool>(nullable: false),
                    PartnerProfile = table.Column<bool>(nullable: false),
                    MessagesInbox = table.Column<bool>(nullable: false),
                    BrandShowcase = table.Column<bool>(nullable: false),
                    PreferredPlaces = table.Column<bool>(nullable: false),
                    Wallet = table.Column<bool>(nullable: false),
                    WorkingHours = table.Column<bool>(nullable: false),
                    PaymentMethods = table.Column<bool>(nullable: false),
                    CustomServices = table.Column<bool>(nullable: false),
                    SocialSites = table.Column<bool>(nullable: false),
                    Certification = table.Column<bool>(nullable: false),
                    Profile = table.Column<bool>(nullable: false),
                    Specialisation = table.Column<bool>(nullable: false),
                    Address = table.Column<bool>(nullable: false),
                    MultipleAssemblies = table.Column<bool>(nullable: false),
                    MultipleCities = table.Column<bool>(nullable: false),
                    Company = table.Column<bool>(nullable: false),
                    Communication = table.Column<bool>(nullable: false),
                    SocialShareButtons = table.Column<bool>(nullable: false),
                    Reviews = table.Column<bool>(nullable: false),
                    LikeDislike = table.Column<bool>(nullable: false),
                    FollowListing = table.Column<bool>(nullable: false),
                    CSSClassName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.PlanID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "plan",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductType = table.Column<string>(nullable: false),
                    PlanID = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "SMSPlans",
                schema: "plan",
                columns: table => new
                {
                    PlanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSPlans", x => x.PlanID);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                schema: "plan",
                columns: table => new
                {
                    SubscriptionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    PeriodID = table.Column<int>(nullable: false),
                    PaymentMethod = table.Column<string>(nullable: false),
                    PaymentStatus = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.SubscriptionID);
                    table.ForeignKey(
                        name: "FK_Subscription_Period_PeriodID",
                        column: x => x.PeriodID,
                        principalSchema: "plan",
                        principalTable: "Period",
                        principalColumn: "PeriodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscription_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "plan",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_PeriodID",
                schema: "plan",
                table: "Subscription",
                column: "PeriodID");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_ProductID",
                schema: "plan",
                table: "Subscription",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertisementPlan",
                schema: "plan");

            migrationBuilder.DropTable(
                name: "BannerPlan",
                schema: "plan");

            migrationBuilder.DropTable(
                name: "DataPlan",
                schema: "plan");

            migrationBuilder.DropTable(
                name: "EmailPlans",
                schema: "plan");

            migrationBuilder.DropTable(
                name: "MagazinePlan",
                schema: "plan");

            migrationBuilder.DropTable(
                name: "Plan",
                schema: "plan");

            migrationBuilder.DropTable(
                name: "SMSPlans",
                schema: "plan");

            migrationBuilder.DropTable(
                name: "Subscription",
                schema: "plan");

            migrationBuilder.DropTable(
                name: "Period",
                schema: "plan");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "plan");
        }
    }
}
