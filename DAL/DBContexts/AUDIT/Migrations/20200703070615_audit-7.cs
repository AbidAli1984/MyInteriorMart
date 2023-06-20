using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class audit7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Activity",
                schema: "audit",
                table: "UserHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Activity",
                schema: "audit",
                table: "ListingLastUpdated",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                schema: "audit",
                table: "UserHistory");

            migrationBuilder.DropColumn(
                name: "Activity",
                schema: "audit",
                table: "ListingLastUpdated");
        }
    }
}
