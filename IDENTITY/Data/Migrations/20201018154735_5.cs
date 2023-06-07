using Microsoft.EntityFrameworkCore.Migrations;

namespace IDENTITY.Data.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "dbo",
                table: "Permission");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "dbo",
                table: "Permission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 2,
                column: "SortOrder",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 3,
                column: "SortOrder",
                value: 2);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 4,
                column: "SortOrder",
                value: 3);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 5,
                column: "SortOrder",
                value: 4);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 6,
                column: "SortOrder",
                value: 5);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 7,
                column: "SortOrder",
                value: 6);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 8,
                column: "SortOrder",
                value: 7);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 9,
                column: "SortOrder",
                value: 8);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Permission",
                keyColumn: "PermissionID",
                keyValue: 10,
                column: "SortOrder",
                value: 9);
        }
    }
}
