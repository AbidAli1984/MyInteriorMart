using Microsoft.EntityFrameworkCore.Migrations;

namespace IDENTITY.Data.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                schema: "dbo",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "ProfileURL",
                schema: "dbo",
                table: "UserProfile");

            migrationBuilder.AddColumn<int>(
                name: "CityID",
                schema: "dbo",
                table: "UserProfile",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityID",
                schema: "dbo",
                table: "UserProfile");

            migrationBuilder.AddColumn<int>(
                name: "City",
                schema: "dbo",
                table: "UserProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfileURL",
                schema: "dbo",
                table: "UserProfile",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
