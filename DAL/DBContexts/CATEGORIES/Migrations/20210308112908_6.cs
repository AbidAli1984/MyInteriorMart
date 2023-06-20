using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.CATEGORIES.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchKeywordName",
                schema: "cat",
                table: "ListingTitle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "ListingTitle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
