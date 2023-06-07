using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Email",
                schema: "audit",
                table: "ListingClaim",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mobile",
                schema: "audit",
                table: "ListingClaim",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationDate",
                schema: "audit",
                table: "ListingClaim",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "audit",
                table: "ListingClaim");

            migrationBuilder.DropColumn(
                name: "Mobile",
                schema: "audit",
                table: "ListingClaim");

            migrationBuilder.DropColumn(
                name: "VerificationDate",
                schema: "audit",
                table: "ListingClaim");
        }
    }
}
