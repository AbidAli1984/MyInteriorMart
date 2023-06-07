using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class audit5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateddDate",
                schema: "audit",
                table: "ListingLastUpdated");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedTime",
                schema: "audit",
                table: "ListingLastUpdated",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "IPAddress",
                schema: "audit",
                table: "ListingLastUpdated",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "audit",
                table: "ListingLastUpdated",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                schema: "audit",
                table: "ListingLastUpdated",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedDate",
                schema: "audit",
                table: "ListingLastUpdated",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "audit",
                table: "ListingLastUpdated");

            migrationBuilder.DropColumn(
                name: "Mobile",
                schema: "audit",
                table: "ListingLastUpdated");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "audit",
                table: "ListingLastUpdated");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                schema: "audit",
                table: "ListingLastUpdated",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IPAddress",
                schema: "audit",
                table: "ListingLastUpdated",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateddDate",
                schema: "audit",
                table: "ListingLastUpdated",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
