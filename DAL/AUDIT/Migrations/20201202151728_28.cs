using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListingID",
                schema: "notif",
                table: "ListingNotification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListingID",
                schema: "notif",
                table: "ListingNotification",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
