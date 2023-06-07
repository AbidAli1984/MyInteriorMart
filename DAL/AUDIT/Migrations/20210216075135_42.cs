using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _42 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLoginOTPVerification",
                schema: "audit",
                columns: table => new
                {
                    OtpID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Mobile = table.Column<string>(nullable: true),
                    OTP = table.Column<int>(nullable: false),
                    OTPVerified = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginOTPVerification", x => x.OtpID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLoginOTPVerification",
                schema: "audit");
        }
    }
}
