using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.LISTING.Migrations
{
    public partial class ChangeColumnTypeToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cast",
                table: "OwnerImage");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "OwnerImage");

            migrationBuilder.AddColumn<int>(
                name: "CastId",
                table: "OwnerImage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReligionId",
                table: "OwnerImage",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CastId",
                table: "OwnerImage");

            migrationBuilder.DropColumn(
                name: "ReligionId",
                table: "OwnerImage");

            migrationBuilder.AddColumn<string>(
                name: "Cast",
                table: "OwnerImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "OwnerImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
