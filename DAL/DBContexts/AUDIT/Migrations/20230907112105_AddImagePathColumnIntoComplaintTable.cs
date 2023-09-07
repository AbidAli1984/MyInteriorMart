using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.AUDIT.Migrations
{
    public partial class AddImagePathColumnIntoComplaintTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Complaints",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Complaints");
        }
    }
}
