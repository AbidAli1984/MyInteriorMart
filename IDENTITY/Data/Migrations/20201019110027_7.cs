using Microsoft.EntityFrameworkCore.Migrations;

namespace IDENTITY.Data.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Permission",
                columns: new[] { "PermissionID", "Approve", "Area", "Controller", "Create", "Delete", "Edit", "Email", "Name", "Project", "Reject", "SMS", "View", "ViewAll" },
                values: new object[,]
                {
                    { 11, null, "Users & Roles", "Manage Users", null, null, null, null, "View", "ADMIN", null, null, "admin.usersandroles.manageusers.view", null },
                    { 12, null, "Users & Roles", "Manage Roles", null, null, null, null, "View", "ADMIN", null, null, "admin.usersandroles.manageroles.view", null },
                    { 13, null, "Users & Roles", "Manage Roles", null, null, null, null, "Edit", "ADMIN", null, null, "admin.usersandroles.manageroles.edit", null },
                    { 14, null, "Users & Roles", "Manage Roles", null, null, null, null, "Remove From Role", "ADMIN", null, null, "admin.usersandroles.removefromrole.edit", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 14);
        }
    }
}
