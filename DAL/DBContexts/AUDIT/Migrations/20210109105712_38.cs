using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _38 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaimType",
                schema: "audit",
                table: "ListingClaim",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimType",
                schema: "audit",
                table: "ListingClaim");
        }
    }
}
