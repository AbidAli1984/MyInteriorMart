using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.USER.Migrations
{
    public partial class AddedIsProfileCompletedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProfileCompleted",
                schema: "dbo",
                table: "UserProfile",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProfileCompleted",
                schema: "dbo",
                table: "UserProfile");
        }
    }
}
