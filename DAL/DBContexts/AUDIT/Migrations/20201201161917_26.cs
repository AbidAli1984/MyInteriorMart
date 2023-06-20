using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListingID",
                schema: "notif",
                table: "ListingNotification",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListingID",
                schema: "notif",
                table: "ListingNotification");
        }
    }
}
