using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.USER.Migrations
{
    public partial class RemoveColIsOtpVerifiedAddedColIsRegistrationCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOtpVerified",
                table: "AspNetUsers",
                newName: "IsRegistrationCompleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRegistrationCompleted",
                table: "AspNetUsers",
                newName: "IsOtpVerified");
        }
    }
}
