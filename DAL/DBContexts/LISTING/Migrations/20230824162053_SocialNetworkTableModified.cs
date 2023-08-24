using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.LISTING.Migrations
{
    public partial class SocialNetworkTableModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Others",
                schema: "listing",
                table: "SocialNetwork");

            migrationBuilder.DropColumn(
                name: "Others1",
                schema: "listing",
                table: "SocialNetwork");

            migrationBuilder.DropColumn(
                name: "Telegram",
                schema: "listing",
                table: "SocialNetwork");

            migrationBuilder.RenameTable(
                name: "SocialNetwork",
                schema: "listing",
                newName: "SocialNetwork");

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "SocialNetwork",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "SocialNetwork");

            migrationBuilder.RenameTable(
                name: "SocialNetwork",
                newName: "SocialNetwork",
                newSchema: "listing");

            migrationBuilder.AddColumn<string>(
                name: "Others",
                schema: "listing",
                table: "SocialNetwork",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Others1",
                schema: "listing",
                table: "SocialNetwork",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telegram",
                schema: "listing",
                table: "SocialNetwork",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
