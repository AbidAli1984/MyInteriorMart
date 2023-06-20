using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.BANNER.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BannerPageID",
                schema: "banner",
                table: "BannerSpace",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BannerSpace_BannerPageID",
                schema: "banner",
                table: "BannerSpace",
                column: "BannerPageID");

            migrationBuilder.AddForeignKey(
                name: "FK_BannerSpace_BannerPage_BannerPageID",
                schema: "banner",
                table: "BannerSpace",
                column: "BannerPageID",
                principalSchema: "banner",
                principalTable: "BannerPage",
                principalColumn: "BannerPageID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannerSpace_BannerPage_BannerPageID",
                schema: "banner",
                table: "BannerSpace");

            migrationBuilder.DropIndex(
                name: "IX_BannerSpace_BannerPageID",
                schema: "banner",
                table: "BannerSpace");

            migrationBuilder.DropColumn(
                name: "BannerPageID",
                schema: "banner",
                table: "BannerSpace");
        }
    }
}
