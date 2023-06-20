using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.BANNER.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "banner");

            migrationBuilder.CreateTable(
                name: "BannerPage",
                schema: "banner",
                columns: table => new
                {
                    BannerPageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerPage", x => x.BannerPageID);
                });

            migrationBuilder.CreateTable(
                name: "BannerSpace",
                schema: "banner",
                columns: table => new
                {
                    BannerSpaceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpaceName = table.Column<string>(nullable: false),
                    Width = table.Column<string>(nullable: false),
                    Height = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerSpace", x => x.BannerSpaceID);
                });

            migrationBuilder.CreateTable(
                name: "BannerType",
                schema: "banner",
                columns: table => new
                {
                    BannerTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerType", x => x.BannerTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                schema: "banner",
                columns: table => new
                {
                    CampaignID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGUID = table.Column<string>(nullable: false),
                    CampaignName = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ImageAltText = table.Column<string>(nullable: false),
                    DestinationURL = table.Column<string>(nullable: false),
                    VideoURL = table.Column<string>(nullable: true),
                    HTML5BannerAdScript = table.Column<string>(nullable: true),
                    BannerTypeID = table.Column<int>(nullable: false),
                    BannerPageID = table.Column<int>(nullable: false),
                    BannerSpaceID = table.Column<int>(nullable: false),
                    ImpressionsCount = table.Column<int>(nullable: false),
                    ClicksCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.CampaignID);
                    table.ForeignKey(
                        name: "FK_Campaign_BannerPage_BannerPageID",
                        column: x => x.BannerPageID,
                        principalSchema: "banner",
                        principalTable: "BannerPage",
                        principalColumn: "BannerPageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Campaign_BannerSpace_BannerSpaceID",
                        column: x => x.BannerSpaceID,
                        principalSchema: "banner",
                        principalTable: "BannerSpace",
                        principalColumn: "BannerSpaceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Campaign_BannerType_BannerTypeID",
                        column: x => x.BannerTypeID,
                        principalSchema: "banner",
                        principalTable: "BannerType",
                        principalColumn: "BannerTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_BannerPageID",
                schema: "banner",
                table: "Campaign",
                column: "BannerPageID");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_BannerSpaceID",
                schema: "banner",
                table: "Campaign",
                column: "BannerSpaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_BannerTypeID",
                schema: "banner",
                table: "Campaign",
                column: "BannerTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campaign",
                schema: "banner");

            migrationBuilder.DropTable(
                name: "BannerPage",
                schema: "banner");

            migrationBuilder.DropTable(
                name: "BannerSpace",
                schema: "banner");

            migrationBuilder.DropTable(
                name: "BannerType",
                schema: "banner");
        }
    }
}
