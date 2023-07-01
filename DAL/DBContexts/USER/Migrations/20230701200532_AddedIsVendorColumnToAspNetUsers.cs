using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.USER.Migrations
{
    public partial class AddedIsVendorColumnToAspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVendor",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVendor",
                table: "AspNetUsers");
        }
    }
}
