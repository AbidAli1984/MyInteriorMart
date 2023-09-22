using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.LISTING.Migrations
{
    public partial class AddOneToOneBetweenAddressAndListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Address_ListingID",
                schema: "listing",
                table: "Address",
                column: "ListingID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Listing_ListingID",
                schema: "listing",
                table: "Address",
                column: "ListingID",
                principalSchema: "listing",
                principalTable: "Listing",
                principalColumn: "ListingID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Listing_ListingID",
                schema: "listing",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_ListingID",
                schema: "listing",
                table: "Address");
        }
    }
}
