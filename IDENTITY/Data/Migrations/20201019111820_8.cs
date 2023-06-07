using Microsoft.EntityFrameworkCore.Migrations;

namespace IDENTITY.Data.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permission",
                schema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "dbo",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Approve = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Create = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Delete = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    View = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewAll = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionID);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Permission",
                columns: new[] { "PermissionID", "Approve", "Area", "Controller", "Create", "Delete", "Edit", "Email", "Name", "Project", "Reject", "SMS", "View", "ViewAll" },
                values: new object[,]
                {
                    { 1, null, "Dashboards", "Realtime", null, null, null, null, "View", "ADMIN", null, null, "admin.dashboard.realtime.view", null },
                    { 2, null, "Dashboards", "Listings", null, null, null, null, "View", "ADMIN", null, null, "admin.dashboard.listings.view", null },
                    { 3, null, "Dashboards", "Users", null, null, null, null, "View", "ADMIN", null, null, "admin.dashboard.users.view", null },
                    { 4, null, "Dashboards", "Analytics", null, null, null, null, "View", "ADMIN", null, null, "admin.dashboard.analytics.view", null },
                    { 5, null, "Dashboards", "Notifications", null, null, null, null, "View", "ADMIN", null, null, "admin.dashboard.notifications.view", null },
                    { 6, null, "Dashboards", "Search", null, null, null, null, "View", "ADMIN", null, null, "admin.dashboard.search.view", null },
                    { 7, null, "Dashboards", "Enquiries", null, null, null, null, "View", "ADMIN", null, null, "admin.dashboard.enquiries.view", null },
                    { 8, null, "Dashboards", "Marketing", null, null, null, null, "View", "ADMIN", null, null, "admin.dashboard.marketing.view", null },
                    { 9, null, "Dashboards", "Billing", null, null, null, null, "View", "ADMIN", null, null, "admin.dashboard.billing.view", null },
                    { 10, null, "Dashboards", "Staff", null, null, null, null, "View", "ADMIN", null, null, "admin.dashboard.staff.view", null },
                    { 11, null, "Users & Roles", "Manage Users", null, null, null, null, "View", "ADMIN", null, null, "admin.usersandroles.manageusers.view", null },
                    { 12, null, "Users & Roles", "Manage Roles", null, null, null, null, "View", "ADMIN", null, null, "admin.usersandroles.manageroles.view", null },
                    { 13, null, "Users & Roles", "Manage Roles", null, null, null, null, "Edit", "ADMIN", null, null, "admin.usersandroles.manageroles.edit", null },
                    { 14, null, "Users & Roles", "Manage Roles", null, null, null, null, "Remove From Role", "ADMIN", null, null, "admin.usersandroles.removefromrole.edit", null }
                });
        }
    }
}
