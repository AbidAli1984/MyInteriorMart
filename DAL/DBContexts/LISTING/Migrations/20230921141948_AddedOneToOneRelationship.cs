using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.LISTING.Migrations
{
    public partial class AddedOneToOneRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LogoImage_ListingID",
                table: "LogoImage",
                column: "ListingID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LogoImage_Listing_ListingID",
                table: "LogoImage",
                column: "ListingID",
                principalSchema: "listing",
                principalTable: "Listing",
                principalColumn: "ListingID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogoImage_Listing_ListingID",
                table: "LogoImage");

            migrationBuilder.DropIndex(
                name: "IX_LogoImage_ListingID",
                table: "LogoImage");
        }
    }
}
