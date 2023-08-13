using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.SHARED.Migrations
{
    public partial class RemoveColumnStationIdFromPincodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StationID",
                schema: "shared",
                table: "Pincode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StationID",
                schema: "shared",
                table: "Pincode",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
