using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.LISTING.Migrations
{
    public partial class _18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListingViewCount",
                schema: "listing",
                columns: table => new
                {
                    ViewCountID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingViewCount", x => x.ViewCountID);
                });

            migrationBuilder.CreateTable(
                name: "ListingViews",
                schema: "listing",
                columns: table => new
                {
                    ListingViewID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingViews", x => x.ListingViewID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingViewCount",
                schema: "listing");

            migrationBuilder.DropTable(
                name: "ListingViews",
                schema: "listing");
        }
    }
}
