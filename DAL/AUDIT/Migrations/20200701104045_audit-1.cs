using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class audit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "audit");

            migrationBuilder.CreateTable(
                name: "ListingLastUpdated",
                schema: "audit",
                columns: table => new
                {
                    LastUpdatedID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    UpdatedByGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    UpdateddDate = table.Column<DateTime>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: false),
                    UserAgent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingLastUpdated", x => x.LastUpdatedID);
                });

            migrationBuilder.CreateTable(
                name: "UserHistory",
                schema: "audit",
                columns: table => new
                {
                    HistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    IPAddress = table.Column<string>(nullable: false),
                    VisitDate = table.Column<DateTime>(nullable: false),
                    VisitTime = table.Column<DateTime>(nullable: false),
                    UserAgent = table.Column<string>(nullable: true),
                    ReferrerURL = table.Column<string>(nullable: true),
                    VisitedURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHistory", x => x.HistoryID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingLastUpdated",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "UserHistory",
                schema: "audit");
        }
    }
}
