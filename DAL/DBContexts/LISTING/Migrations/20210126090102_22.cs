using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.LISTING.Migrations
{
    public partial class _22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Turnover",
                schema: "listing",
                table: "Listing",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                schema: "listing",
                table: "Listing",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedOrRejectedBy",
                schema: "listing",
                table: "Listing",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Rejected",
                schema: "listing",
                table: "Listing",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                schema: "listing",
                table: "Listing");

            migrationBuilder.DropColumn(
                name: "ApprovedOrRejectedBy",
                schema: "listing",
                table: "Listing");

            migrationBuilder.DropColumn(
                name: "Rejected",
                schema: "listing",
                table: "Listing");

            migrationBuilder.AlterColumn<string>(
                name: "Turnover",
                schema: "listing",
                table: "Listing",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
