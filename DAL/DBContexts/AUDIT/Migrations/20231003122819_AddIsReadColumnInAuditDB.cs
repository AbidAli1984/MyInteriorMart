using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.AUDIT.Migrations
{
    public partial class AddIsReadColumnInAuditDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                schema: "audit",
                table: "Subscribes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                schema: "audit",
                table: "LikeDislike",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                schema: "audit",
                table: "Bookmarks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                schema: "audit",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "IsRead",
                schema: "audit",
                table: "LikeDislike");

            migrationBuilder.DropColumn(
                name: "IsRead",
                schema: "audit",
                table: "Bookmarks");
        }
    }
}
