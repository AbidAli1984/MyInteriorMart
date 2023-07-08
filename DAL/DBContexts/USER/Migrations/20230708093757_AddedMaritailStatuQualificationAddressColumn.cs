using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.USER.Migrations
{
    public partial class AddedMaritailStatuQualificationAddressColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                schema: "dbo",
                table: "UserProfile");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "dbo",
                table: "UserProfile",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                schema: "dbo",
                table: "UserProfile",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                schema: "dbo",
                table: "UserProfile",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "dbo",
                table: "UserProfile",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "dbo",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                schema: "dbo",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Qualification",
                schema: "dbo",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "UserProfile");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                schema: "dbo",
                table: "UserProfile",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
