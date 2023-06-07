using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IDENTITY.Data.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SuspendedUser",
                schema: "dbo",
                columns: table => new
                {
                    SuspendedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuspendedTo = table.Column<string>(nullable: false),
                    SuspendedBy = table.Column<string>(nullable: false),
                    SuspendedDate = table.Column<DateTime>(nullable: false),
                    Suspended = table.Column<bool>(nullable: false),
                    ReasonForSuspending = table.Column<string>(nullable: true),
                    UnsuspendedDate = table.Column<DateTime>(nullable: false),
                    UnsuspendedBy = table.Column<string>(nullable: false),
                    ReasonForUnsuspending = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuspendedUser", x => x.SuspendedId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuspendedUser",
                schema: "dbo");
        }
    }
}
