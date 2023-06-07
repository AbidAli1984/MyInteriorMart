using Microsoft.EntityFrameworkCore.Migrations;

namespace IDENTITY.Data.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "dbo",
                columns: table => new
                {
                    PermissionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Claim = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionID);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Permission",
                columns: new[] { "PermissionID", "Area", "Claim", "Controller", "Name", "Project", "SortOrder" },
                values: new object[] { 1, "Dashboards", "admin.dashboard.analytics.view", "Analytics", "View", "ADMIN", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permission",
                schema: "dbo");
        }
    }
}
