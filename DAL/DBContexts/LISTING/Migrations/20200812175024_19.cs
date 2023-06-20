using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.LISTING.Migrations
{
    public partial class _19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserType",
                schema: "listing",
                table: "ListingViews",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                schema: "listing",
                table: "ListingViews");
        }
    }
}
