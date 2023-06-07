using Microsoft.EntityFrameworkCore.Migrations;

namespace IDENTITY.Data.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                schema: "dbo",
                table: "RoleCategory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "dbo",
                table: "RoleCategory");
        }
    }
}
