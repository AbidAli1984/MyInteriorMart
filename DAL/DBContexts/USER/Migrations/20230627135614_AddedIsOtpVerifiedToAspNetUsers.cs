using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.USER.Migrations
{
    public partial class AddedIsOtpVerifiedToAspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOtpVerified",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOtpVerified",
                table: "AspNetUsers");
        }
    }
}
