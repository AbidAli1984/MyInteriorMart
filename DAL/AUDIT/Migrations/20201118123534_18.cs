using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "audit",
                table: "SearchHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                schema: "audit",
                table: "SearchHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                schema: "audit",
                table: "SearchHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                schema: "audit",
                table: "SearchHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pincode",
                schema: "audit",
                table: "SearchHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "audit",
                table: "SearchHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                schema: "audit",
                table: "SearchHistory");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                schema: "audit",
                table: "SearchHistory");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "audit",
                table: "SearchHistory");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "audit",
                table: "SearchHistory");

            migrationBuilder.DropColumn(
                name: "Pincode",
                schema: "audit",
                table: "SearchHistory");

            migrationBuilder.DropColumn(
                name: "State",
                schema: "audit",
                table: "SearchHistory");
        }
    }
}
