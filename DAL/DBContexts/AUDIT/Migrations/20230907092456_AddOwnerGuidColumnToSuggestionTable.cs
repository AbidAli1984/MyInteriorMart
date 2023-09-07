using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.AUDIT.Migrations
{
    public partial class AddOwnerGuidColumnToSuggestionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerGuid",
                table: "Suggestions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerGuid",
                table: "Suggestions");
        }
    }
}
