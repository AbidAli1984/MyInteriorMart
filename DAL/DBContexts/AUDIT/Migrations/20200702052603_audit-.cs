using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class audit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitTime",
                schema: "audit",
                table: "UserHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VisitTime",
                schema: "audit",
                table: "UserHistory",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
