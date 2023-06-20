using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _39 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentApprovedDisapprovedReasonByStaff",
                schema: "audit",
                table: "ListingClaim",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentScrutinizedByStaffGuid",
                schema: "audit",
                table: "ListingClaim",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentApprovedDisapprovedReasonByStaff",
                schema: "audit",
                table: "ListingClaim");

            migrationBuilder.DropColumn(
                name: "DocumentScrutinizedByStaffGuid",
                schema: "audit",
                table: "ListingClaim");
        }
    }
}
