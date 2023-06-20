using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.LISTING.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptTenderWork",
                schema: "listing",
                table: "Specialisation",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptTenderWork",
                schema: "listing",
                table: "Specialisation");
        }
    }
}
