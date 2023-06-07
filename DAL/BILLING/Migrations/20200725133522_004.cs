using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.BILLING.Migrations
{
    public partial class _004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanAmount",
                schema: "plan",
                table: "Product",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanAmount",
                schema: "plan",
                table: "Product");
        }
    }
}
