using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.LISTING.Migrations
{
    public partial class CreateRatingReplyTableTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RatingReply_RatingId",
                table: "RatingReply");

            migrationBuilder.CreateIndex(
                name: "IX_RatingReply_RatingId",
                table: "RatingReply",
                column: "RatingId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RatingReply_RatingId",
                table: "RatingReply");

            migrationBuilder.CreateIndex(
                name: "IX_RatingReply_RatingId",
                table: "RatingReply",
                column: "RatingId");
        }
    }
}
