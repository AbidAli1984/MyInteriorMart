using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.LISTING.Migrations
{
    public partial class AddedBannerCertificationClientDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BannerDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerGuid = table.Column<string>(nullable: false),
                    ListingID = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(nullable: false),
                    ImageTitle = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CertificationDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerGuid = table.Column<string>(nullable: false),
                    ListingID = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(nullable: false),
                    ImageTitle = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerGuid = table.Column<string>(nullable: false),
                    ListingID = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(nullable: false),
                    ImageTitle = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDetail", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerDetail");

            migrationBuilder.DropTable(
                name: "CertificationDetail");

            migrationBuilder.DropTable(
                name: "ClientDetail");
        }
    }
}
