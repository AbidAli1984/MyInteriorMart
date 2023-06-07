using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Dislike",
                schema: "audit",
                table: "LikeDislike",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Like",
                schema: "audit",
                table: "LikeDislike",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislike",
                schema: "audit",
                table: "LikeDislike");

            migrationBuilder.DropColumn(
                name: "Like",
                schema: "audit",
                table: "LikeDislike");
        }
    }
}
