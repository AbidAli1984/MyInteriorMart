using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.LISTING.Migrations
{
    public partial class RenameColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CastId",
                table: "OwnerImage");

            migrationBuilder.AddColumn<int>(
                name: "CasteId",
                table: "OwnerImage",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CasteId",
                table: "OwnerImage");

            migrationBuilder.AddColumn<int>(
                name: "CastId",
                table: "OwnerImage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
