using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.LISTING.Migrations
{
    public partial class _20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "listing",
                table: "ListingViews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "listing",
                table: "ListingViews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IPv4",
                schema: "listing",
                table: "ListingViews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                schema: "listing",
                table: "ListingViews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                schema: "listing",
                table: "ListingViews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pincode",
                schema: "listing",
                table: "ListingViews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "listing",
                table: "ListingViews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                schema: "listing",
                table: "ListingViews");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "listing",
                table: "ListingViews");

            migrationBuilder.DropColumn(
                name: "IPv4",
                schema: "listing",
                table: "ListingViews");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "listing",
                table: "ListingViews");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "listing",
                table: "ListingViews");

            migrationBuilder.DropColumn(
                name: "Pincode",
                schema: "listing",
                table: "ListingViews");

            migrationBuilder.DropColumn(
                name: "State",
                schema: "listing",
                table: "ListingViews");
        }
    }
}
