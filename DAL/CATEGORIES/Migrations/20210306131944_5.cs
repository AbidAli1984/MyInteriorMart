using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.CATEGORIES.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListingTitle",
                schema: "cat",
                columns: table => new
                {
                    TitleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    SecondCategoryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SearchKeywordName = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Keyword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingTitle", x => x.TitleID);
                    table.ForeignKey(
                        name: "FK_ListingTitle_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingTitle_SecondCategory_SecondCategoryID",
                        column: x => x.SecondCategoryID,
                        principalSchema: "cat",
                        principalTable: "SecondCategory",
                        principalColumn: "SecondCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingTitle_FirstCategoryID",
                schema: "cat",
                table: "ListingTitle",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingTitle_SecondCategoryID",
                schema: "cat",
                table: "ListingTitle",
                column: "SecondCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingTitle",
                schema: "cat");
        }
    }
}
