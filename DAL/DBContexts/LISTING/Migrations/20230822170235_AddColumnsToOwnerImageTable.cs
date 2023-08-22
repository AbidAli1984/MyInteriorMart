using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.LISTING.Migrations
{
    public partial class AddColumnsToOwnerImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cast",
                table: "OwnerImage",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CountryID",
                table: "OwnerImage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "OwnerImage",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StateID",
                table: "OwnerImage",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cast",
                table: "OwnerImage");

            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "OwnerImage");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "OwnerImage");

            migrationBuilder.DropColumn(
                name: "StateID",
                table: "OwnerImage");
        }
    }
}
