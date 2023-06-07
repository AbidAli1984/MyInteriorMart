using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.LISTING.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                schema: "listing",
                table: "AssembliesAndCities",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ListingID",
                schema: "listing",
                table: "AssembliesAndCities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerGuid",
                schema: "listing",
                table: "AssembliesAndCities",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IPAddress",
                schema: "listing",
                table: "AssembliesAndCities");

            migrationBuilder.DropColumn(
                name: "ListingID",
                schema: "listing",
                table: "AssembliesAndCities");

            migrationBuilder.DropColumn(
                name: "OwnerGuid",
                schema: "listing",
                table: "AssembliesAndCities");
        }
    }
}
