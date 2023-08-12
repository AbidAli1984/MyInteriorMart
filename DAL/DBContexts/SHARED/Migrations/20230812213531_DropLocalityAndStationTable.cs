using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.SHARED.Migrations
{
    public partial class DropLocalityAndStationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pincode_Station_StationID",
                schema: "shared",
                table: "Pincode");

            migrationBuilder.DropTable(
                name: "Locality",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "Station",
                schema: "shared");

            migrationBuilder.DropIndex(
                name: "IX_Pincode_StationID",
                schema: "shared",
                table: "Pincode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Station",
                schema: "shared",
                columns: table => new
                {
                    StationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.StationID);
                    table.ForeignKey(
                        name: "FK_Station_City_CityID",
                        column: x => x.CityID,
                        principalSchema: "shared",
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locality",
                schema: "shared",
                columns: table => new
                {
                    LocalityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PincodeID = table.Column<int>(type: "int", nullable: false),
                    StationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locality", x => x.LocalityID);
                    table.ForeignKey(
                        name: "FK_Locality_Pincode_PincodeID",
                        column: x => x.PincodeID,
                        principalSchema: "shared",
                        principalTable: "Pincode",
                        principalColumn: "PincodeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locality_Station_StationID",
                        column: x => x.StationID,
                        principalSchema: "shared",
                        principalTable: "Station",
                        principalColumn: "StationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pincode_StationID",
                schema: "shared",
                table: "Pincode",
                column: "StationID");

            migrationBuilder.CreateIndex(
                name: "IX_Locality_PincodeID",
                schema: "shared",
                table: "Locality",
                column: "PincodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Locality_StationID",
                schema: "shared",
                table: "Locality",
                column: "StationID");

            migrationBuilder.CreateIndex(
                name: "IX_Station_CityID",
                schema: "shared",
                table: "Station",
                column: "CityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pincode_Station_StationID",
                schema: "shared",
                table: "Pincode",
                column: "StationID",
                principalSchema: "shared",
                principalTable: "Station",
                principalColumn: "StationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
