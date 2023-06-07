using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.BANNER.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                schema: "banner",
                table: "Campaign",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "banner",
                table: "Campaign",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "banner",
                table: "Campaign");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "banner",
                table: "Campaign");
        }
    }
}
