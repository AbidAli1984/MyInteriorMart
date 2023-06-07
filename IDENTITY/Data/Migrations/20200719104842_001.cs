using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IDENTITY.Data.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "UserProfile",
                schema: "dbo",
                columns: table => new
                {
                    ProfileID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    CountryID = table.Column<int>(nullable: false),
                    StateID = table.Column<int>(nullable: false),
                    City = table.Column<int>(nullable: false),
                    AssemblyID = table.Column<int>(nullable: false),
                    PincodeID = table.Column<int>(nullable: false),
                    TimeZoneOfCountry = table.Column<string>(nullable: false),
                    ProfileURL = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.ProfileID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfile",
                schema: "dbo");
        }
    }
}
