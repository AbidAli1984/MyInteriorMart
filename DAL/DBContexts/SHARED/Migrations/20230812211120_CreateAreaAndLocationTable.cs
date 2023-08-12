using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.SHARED.Migrations
{
    public partial class CreateAreaAndLocationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                schema: "shared",
                table: "Pincode",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_City_CityID",
                        column: x => x.CityID,
                        principalSchema: "shared",
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    PincodeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Area_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Area_Pincode_PincodeID",
                        column: x => x.PincodeID,
                        principalSchema: "shared",
                        principalTable: "Pincode",
                        principalColumn: "PincodeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pincode_LocationId",
                schema: "shared",
                table: "Pincode",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Area_LocationId",
                table: "Area",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Area_PincodeID",
                table: "Area",
                column: "PincodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Location_CityID",
                table: "Location",
                column: "CityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pincode_Location_LocationId",
                schema: "shared",
                table: "Pincode",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pincode_Location_LocationId",
                schema: "shared",
                table: "Pincode");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Pincode_LocationId",
                schema: "shared",
                table: "Pincode");

            migrationBuilder.DropColumn(
                name: "LocationId",
                schema: "shared",
                table: "Pincode");
        }
    }
}
