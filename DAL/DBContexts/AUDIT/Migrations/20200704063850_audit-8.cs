using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class audit8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                schema: "audit",
                table: "UserHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SectionUpdated",
                schema: "audit",
                table: "ListingLastUpdated",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                schema: "audit",
                table: "ListingLastUpdated",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                schema: "audit",
                table: "UserHistory");

            migrationBuilder.DropColumn(
                name: "SectionUpdated",
                schema: "audit",
                table: "ListingLastUpdated");

            migrationBuilder.DropColumn(
                name: "UserRole",
                schema: "audit",
                table: "ListingLastUpdated");
        }
    }
}
