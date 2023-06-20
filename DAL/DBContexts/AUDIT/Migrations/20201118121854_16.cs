using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchHistory",
                schema: "audit",
                columns: table => new
                {
                    HistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: false),
                    VisitTime = table.Column<DateTime>(nullable: false),
                    SearchTerm = table.Column<string>(nullable: true),
                    SearchID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchHistory", x => x.HistoryID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchHistory",
                schema: "audit");
        }
    }
}
