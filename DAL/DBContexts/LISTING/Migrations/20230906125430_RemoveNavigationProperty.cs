using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.LISTING.Migrations
{
    public partial class RemoveNavigationProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingReply_Rating_RatingId",
                table: "RatingReply");

            migrationBuilder.DropIndex(
                name: "IX_RatingReply_RatingId",
                table: "RatingReply");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RatingReply_RatingId",
                table: "RatingReply",
                column: "RatingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReply_Rating_RatingId",
                table: "RatingReply",
                column: "RatingId",
                principalSchema: "listing",
                principalTable: "Rating",
                principalColumn: "RatingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
