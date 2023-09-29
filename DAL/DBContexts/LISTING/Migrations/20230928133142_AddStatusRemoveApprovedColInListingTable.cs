using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.LISTING.Migrations
{
    public partial class AddStatusRemoveApprovedColInListingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                schema: "listing",
                table: "Listing");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "listing",
                table: "Listing",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "listing",
                table: "Listing");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                schema: "listing",
                table: "Listing",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
