using Microsoft.EntityFrameworkCore.Migrations;

namespace IDENTITY.Data.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Claim",
                schema: "dbo",
                table: "Permission");

            migrationBuilder.AddColumn<string>(
                name: "Approve",
                schema: "dbo",
                table: "Permission",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Create",
                schema: "dbo",
                table: "Permission",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Delete",
                schema: "dbo",
                table: "Permission",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Edit",
                schema: "dbo",
                table: "Permission",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "dbo",
                table: "Permission",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reject",
                schema: "dbo",
                table: "Permission",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SMS",
                schema: "dbo",
                table: "Permission",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "View",
                schema: "dbo",
                table: "Permission",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViewAll",
                schema: "dbo",
                table: "Permission",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 1,
                column: "View",
                value: "admin.dashboard.realtime.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 2,
                column: "View",
                value: "admin.dashboard.listings.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 3,
                column: "View",
                value: "admin.dashboard.users.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 4,
                column: "View",
                value: "admin.dashboard.analytics.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 5,
                column: "View",
                value: "admin.dashboard.notifications.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 6,
                column: "View",
                value: "admin.dashboard.search.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 7,
                column: "View",
                value: "admin.dashboard.enquiries.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 8,
                column: "View",
                value: "admin.dashboard.marketing.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 9,
                column: "View",
                value: "admin.dashboard.billing.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 10,
                column: "View",
                value: "admin.dashboard.staff.view");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approve",
                schema: "dbo",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "Create",
                schema: "dbo",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "Delete",
                schema: "dbo",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "Edit",
                schema: "dbo",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "dbo",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "Reject",
                schema: "dbo",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "SMS",
                schema: "dbo",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "View",
                schema: "dbo",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "ViewAll",
                schema: "dbo",
                table: "Permission");

            migrationBuilder.AddColumn<string>(
                name: "Claim",
                schema: "dbo",
                table: "Permission",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 1,
                column: "Claim",
                value: "admin.dashboard.realtime.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 2,
                column: "Claim",
                value: "admin.dashboard.listings.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 3,
                column: "Claim",
                value: "admin.dashboard.users.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 4,
                column: "Claim",
                value: "admin.dashboard.analytics.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 5,
                column: "Claim",
                value: "admin.dashboard.notifications.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 6,
                column: "Claim",
                value: "admin.dashboard.search.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 7,
                column: "Claim",
                value: "admin.dashboard.enquiries.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 8,
                column: "Claim",
                value: "admin.dashboard.marketing.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 9,
                column: "Claim",
                value: "admin.dashboard.billing.view");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 10,
                column: "Claim",
                value: "admin.dashboard.staff.view");
        }
    }
}
