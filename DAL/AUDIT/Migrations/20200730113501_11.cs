using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislike",
                schema: "audit",
                table: "LikeDislike");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Dislike",
                schema: "audit",
                table: "LikeDislike",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
