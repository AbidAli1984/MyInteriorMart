using Microsoft.EntityFrameworkCore.Migrations;

namespace IDENTITY.Data.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleCategory",
                schema: "dbo",
                columns: table => new
                {
                    RoleCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleCategory", x => x.RoleCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "RoleCategoryAndRole",
                columns: table => new
                {
                    RoleCategoryAndRoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleCategoryID = table.Column<int>(nullable: false),
                    RoleID = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleCategoryAndRole", x => x.RoleCategoryAndRoleID);
                    table.ForeignKey(
                        name: "FK_RoleCategoryAndRole_RoleCategory_RoleCategoryID",
                        column: x => x.RoleCategoryID,
                        principalSchema: "dbo",
                        principalTable: "RoleCategory",
                        principalColumn: "RoleCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleCategoryAndRole_RoleCategoryID",
                table: "RoleCategoryAndRole",
                column: "RoleCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleCategoryAndRole");

            migrationBuilder.DropTable(
                name: "RoleCategory",
                schema: "dbo");
        }
    }
}
