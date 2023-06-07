using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchID",
                schema: "audit",
                table: "SearchHistory");

            migrationBuilder.AddColumn<int>(
                name: "SearchTermID",
                schema: "audit",
                table: "SearchHistory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchTermID",
                schema: "audit",
                table: "SearchHistory");

            migrationBuilder.AddColumn<int>(
                name: "SearchID",
                schema: "audit",
                table: "SearchHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
