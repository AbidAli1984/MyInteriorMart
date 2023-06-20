using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaimedBy",
                schema: "audit",
                table: "ListingClaim",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ListingID",
                schema: "audit",
                table: "ListingClaim",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                schema: "audit",
                table: "ListingClaim",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OTPVerified",
                schema: "audit",
                table: "ListingClaim",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "audit",
                table: "ListingClaim",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimedBy",
                schema: "audit",
                table: "ListingClaim");

            migrationBuilder.DropColumn(
                name: "ListingID",
                schema: "audit",
                table: "ListingClaim");

            migrationBuilder.DropColumn(
                name: "Message",
                schema: "audit",
                table: "ListingClaim");

            migrationBuilder.DropColumn(
                name: "OTPVerified",
                schema: "audit",
                table: "ListingClaim");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "audit",
                table: "ListingClaim");
        }
    }
}
