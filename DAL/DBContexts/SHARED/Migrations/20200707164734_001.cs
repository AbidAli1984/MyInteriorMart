using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.SHARED.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "shared",
                columns: table => new
                {
                    CountryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    SortName = table.Column<string>(nullable: false),
                    ISO3Name = table.Column<string>(nullable: false),
                    Capital = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    PhoneCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Designation",
                schema: "shared",
                columns: table => new
                {
                    DesignationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designation", x => x.DesignationID);
                });

            migrationBuilder.CreateTable(
                name: "NatureOfBusiness",
                schema: "shared",
                columns: table => new
                {
                    NatureOfBusinessID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NatureOfBusiness", x => x.NatureOfBusinessID);
                });

            migrationBuilder.CreateTable(
                name: "Turnover",
                schema: "shared",
                columns: table => new
                {
                    TurnoverID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnover", x => x.TurnoverID);
                });

            migrationBuilder.CreateTable(
                name: "State",
                schema: "shared",
                columns: table => new
                {
                    StateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CountryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateID);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "shared",
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "shared",
                columns: table => new
                {
                    CityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryID = table.Column<int>(nullable: false),
                    StateID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "shared",
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_City_State_StateID",
                        column: x => x.StateID,
                        principalSchema: "shared",
                        principalTable: "State",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                schema: "shared",
                columns: table => new
                {
                    StationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CityID = table.Column<int>(nullable: false)
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Pincode",
                schema: "shared",
                columns: table => new
                {
                    PincodeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PincodeNumber = table.Column<int>(nullable: false),
                    StationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pincode", x => x.PincodeID);
                    table.ForeignKey(
                        name: "FK_Pincode_Station_StationID",
                        column: x => x.StationID,
                        principalSchema: "shared",
                        principalTable: "Station",
                        principalColumn: "StationID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Locality",
                schema: "shared",
                columns: table => new
                {
                    LocalityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalityName = table.Column<string>(nullable: false),
                    StationID = table.Column<int>(nullable: false),
                    PincodeID = table.Column<int>(nullable: false)
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Locality_Station_StationID",
                        column: x => x.StationID,
                        principalSchema: "shared",
                        principalTable: "Station",
                        principalColumn: "StationID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryID",
                schema: "shared",
                table: "City",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateID",
                schema: "shared",
                table: "City",
                column: "StateID");

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
                name: "IX_Pincode_StationID",
                schema: "shared",
                table: "Pincode",
                column: "StationID");

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryID",
                schema: "shared",
                table: "State",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Station_CityID",
                schema: "shared",
                table: "Station",
                column: "CityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Designation",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "Locality",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "NatureOfBusiness",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "Turnover",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "Pincode",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "Station",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "City",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "State",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "shared");
        }
    }
}
