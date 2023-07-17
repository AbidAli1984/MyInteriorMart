using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.USER.Migrations
{
    public partial class RenameQualificationToQualificationIdAndDataTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qualification",
                schema: "dbo",
                table: "UserProfile");

            migrationBuilder.AddColumn<int>(
                name: "QualificationId",
                schema: "dbo",
                table: "UserProfile",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QualificationId",
                schema: "dbo",
                table: "UserProfile");

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                schema: "dbo",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
