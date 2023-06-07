using Microsoft.EntityFrameworkCore.Migrations;

namespace IDENTITY.Data.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 1,
                columns: new[] { "Claim", "Controller" },
                values: new object[] { "admin.dashboard.realtime.view", "Realtime" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Permission",
                columns: new[] { "PermissionID", "Area", "Claim", "Controller", "Name", "Project", "SortOrder" },
                values: new object[,]
                {
                    { 2, "Dashboards", "admin.dashboard.listings.view", "Listings", "View", "ADMIN", 1 },
                    { 3, "Dashboards", "admin.dashboard.users.view", "Users", "View", "ADMIN", 2 },
                    { 4, "Dashboards", "admin.dashboard.analytics.view", "Analytics", "View", "ADMIN", 3 },
                    { 5, "Dashboards", "admin.dashboard.notifications.view", "Notifications", "View", "ADMIN", 4 },
                    { 6, "Dashboards", "admin.dashboard.search.view", "Search", "View", "ADMIN", 5 },
                    { 7, "Dashboards", "admin.dashboard.enquiries.view", "Enquiries", "View", "ADMIN", 6 },
                    { 8, "Dashboards", "admin.dashboard.marketing.view", "Marketing", "View", "ADMIN", 7 },
                    { 9, "Dashboards", "admin.dashboard.billing.view", "Billing", "View", "ADMIN", 8 },
                    { 10, "Dashboards", "admin.dashboard.staff.view", "Staff", "View", "ADMIN", 9 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 10);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 1,
                columns: new[] { "Claim", "Controller" },
                values: new object[] { "admin.dashboard.analytics.view", "Analytics" });
        }
    }
}
