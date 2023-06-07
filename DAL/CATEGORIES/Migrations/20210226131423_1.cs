using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.CATEGORIES.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "brands");

            migrationBuilder.EnsureSchema(
                name: "cat");

            migrationBuilder.EnsureSchema(
                name: "cms");

            migrationBuilder.CreateTable(
                name: "Brand",
                schema: "brands",
                columns: table => new
                {
                    BrandID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    Descript = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandID);
                });

            migrationBuilder.CreateTable(
                name: "KeywordBrand",
                schema: "brands",
                columns: table => new
                {
                    KeywordBrandID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keyword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordBrand", x => x.KeywordBrandID);
                });

            migrationBuilder.CreateTable(
                name: "FirstCategory",
                schema: "cat",
                columns: table => new
                {
                    FirstCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SearchKeywordName = table.Column<string>(maxLength: 50, nullable: false),
                    URL = table.Column<string>(maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 153, nullable: false),
                    Keyword = table.Column<string>(nullable: true),
                    SearchCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstCategory", x => x.FirstCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "NewSearchTerm",
                schema: "cat",
                columns: table => new
                {
                    NewSearchTermID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchTerm = table.Column<string>(nullable: false),
                    SearchDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewSearchTerm", x => x.NewSearchTermID);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                schema: "cms",
                columns: table => new
                {
                    PageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageName = table.Column<string>(nullable: false),
                    Priority = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Keywords = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageID);
                });

            migrationBuilder.CreateTable(
                name: "KeywordFirstCategory",
                schema: "cat",
                columns: table => new
                {
                    KeywordFirstCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    Keyword = table.Column<string>(maxLength: 100, nullable: false),
                    URL = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 153, nullable: false),
                    Description = table.Column<string>(maxLength: 153, nullable: false),
                    SearchCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordFirstCategory", x => x.KeywordFirstCategoryID);
                    table.ForeignKey(
                        name: "FK_KeywordFirstCategory_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SecondCategory",
                schema: "cat",
                columns: table => new
                {
                    SecondCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SearchKeywordName = table.Column<string>(maxLength: 50, nullable: false),
                    URL = table.Column<string>(maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 153, nullable: false),
                    Keyword = table.Column<string>(nullable: true),
                    SearchCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondCategory", x => x.SecondCategoryID);
                    table.ForeignKey(
                        name: "FK_SecondCategory_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BrandsCategory",
                schema: "brands",
                columns: table => new
                {
                    BrandCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandID = table.Column<int>(nullable: true),
                    FirstCategoryID = table.Column<int>(nullable: true),
                    SecondCategoryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandsCategory", x => x.BrandCategoryID);
                    table.ForeignKey(
                        name: "FK_BrandsCategory_Brand_BrandID",
                        column: x => x.BrandID,
                        principalSchema: "brands",
                        principalTable: "Brand",
                        principalColumn: "BrandID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BrandsCategory_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BrandsCategory_SecondCategory_SecondCategoryID",
                        column: x => x.SecondCategoryID,
                        principalSchema: "cat",
                        principalTable: "SecondCategory",
                        principalColumn: "SecondCategoryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ThirdCategory",
                schema: "cat",
                columns: table => new
                {
                    ThirdCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    SecondCategoryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SearchKeywordName = table.Column<string>(maxLength: 50, nullable: false),
                    URL = table.Column<string>(maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 153, nullable: false),
                    Keyword = table.Column<string>(nullable: true),
                    SearchCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdCategory", x => x.ThirdCategoryID);
                    table.ForeignKey(
                        name: "FK_ThirdCategory_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ThirdCategory_SecondCategory_SecondCategoryID",
                        column: x => x.SecondCategoryID,
                        principalSchema: "cat",
                        principalTable: "SecondCategory",
                        principalColumn: "SecondCategoryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FourthCategory",
                schema: "cat",
                columns: table => new
                {
                    FourthCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    SecondCategoryID = table.Column<int>(nullable: false),
                    ThirdCategoryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SearchKeywordName = table.Column<string>(maxLength: 50, nullable: false),
                    URL = table.Column<string>(maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 153, nullable: false),
                    Keyword = table.Column<string>(nullable: true),
                    SearchCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FourthCategory", x => x.FourthCategoryID);
                    table.ForeignKey(
                        name: "FK_FourthCategory_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FourthCategory_SecondCategory_SecondCategoryID",
                        column: x => x.SecondCategoryID,
                        principalSchema: "cat",
                        principalTable: "SecondCategory",
                        principalColumn: "SecondCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FourthCategory_ThirdCategory_ThirdCategoryID",
                        column: x => x.ThirdCategoryID,
                        principalSchema: "cat",
                        principalTable: "ThirdCategory",
                        principalColumn: "ThirdCategoryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FifthCategory",
                schema: "cat",
                columns: table => new
                {
                    FifthCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    SecondCategoryID = table.Column<int>(nullable: false),
                    ThirdCategoryID = table.Column<int>(nullable: false),
                    FourthCategoryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SearchKeywordName = table.Column<string>(maxLength: 50, nullable: false),
                    URL = table.Column<string>(maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 153, nullable: false),
                    Keyword = table.Column<string>(nullable: true),
                    SearchCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FifthCategory", x => x.FifthCategoryID);
                    table.ForeignKey(
                        name: "FK_FifthCategory_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FifthCategory_FourthCategory_FourthCategoryID",
                        column: x => x.FourthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FourthCategory",
                        principalColumn: "FourthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FifthCategory_SecondCategory_SecondCategoryID",
                        column: x => x.SecondCategoryID,
                        principalSchema: "cat",
                        principalTable: "SecondCategory",
                        principalColumn: "SecondCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FifthCategory_ThirdCategory_ThirdCategoryID",
                        column: x => x.ThirdCategoryID,
                        principalSchema: "cat",
                        principalTable: "ThirdCategory",
                        principalColumn: "ThirdCategoryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "KeywordFifthCategory",
                schema: "cat",
                columns: table => new
                {
                    KeywordFifthCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    SecondCategoryID = table.Column<int>(nullable: false),
                    ThirdCategoryID = table.Column<int>(nullable: false),
                    FourthCategoryID = table.Column<int>(nullable: false),
                    FifthCategoryID = table.Column<int>(nullable: false),
                    Keyword = table.Column<string>(maxLength: 100, nullable: false),
                    URL = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 153, nullable: false),
                    Description = table.Column<string>(maxLength: 153, nullable: false),
                    SearchCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordFifthCategory", x => x.KeywordFifthCategoryID);
                    table.ForeignKey(
                        name: "FK_KeywordFifthCategory_FifthCategory_FifthCategoryID",
                        column: x => x.FifthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FifthCategory",
                        principalColumn: "FifthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordFifthCategory_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordFifthCategory_FourthCategory_FourthCategoryID",
                        column: x => x.FourthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FourthCategory",
                        principalColumn: "FourthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordFifthCategory_SecondCategory_SecondCategoryID",
                        column: x => x.SecondCategoryID,
                        principalSchema: "cat",
                        principalTable: "SecondCategory",
                        principalColumn: "SecondCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordFifthCategory_ThirdCategory_ThirdCategoryID",
                        column: x => x.ThirdCategoryID,
                        principalSchema: "cat",
                        principalTable: "ThirdCategory",
                        principalColumn: "ThirdCategoryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "KeywordFourthCategory",
                schema: "cat",
                columns: table => new
                {
                    KeywordFourthCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    SecondCategoryID = table.Column<int>(nullable: false),
                    ThirdCategoryID = table.Column<int>(nullable: false),
                    FourthCategoryID = table.Column<int>(nullable: false),
                    Keyword = table.Column<string>(maxLength: 100, nullable: false),
                    URL = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 153, nullable: false),
                    Description = table.Column<string>(maxLength: 153, nullable: false),
                    SearchCount = table.Column<int>(nullable: false),
                    FifthCategoryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordFourthCategory", x => x.KeywordFourthCategoryID);
                    table.ForeignKey(
                        name: "FK_KeywordFourthCategory_FifthCategory_FifthCategoryID",
                        column: x => x.FifthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FifthCategory",
                        principalColumn: "FifthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordFourthCategory_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordFourthCategory_FourthCategory_FourthCategoryID",
                        column: x => x.FourthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FourthCategory",
                        principalColumn: "FourthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordFourthCategory_SecondCategory_SecondCategoryID",
                        column: x => x.SecondCategoryID,
                        principalSchema: "cat",
                        principalTable: "SecondCategory",
                        principalColumn: "SecondCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordFourthCategory_ThirdCategory_ThirdCategoryID",
                        column: x => x.ThirdCategoryID,
                        principalSchema: "cat",
                        principalTable: "ThirdCategory",
                        principalColumn: "ThirdCategoryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "KeywordSecondCategory",
                schema: "cat",
                columns: table => new
                {
                    KeywordSecondCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    SecondCategoryID = table.Column<int>(nullable: false),
                    Keyword = table.Column<string>(maxLength: 100, nullable: false),
                    URL = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 153, nullable: false),
                    Description = table.Column<string>(maxLength: 153, nullable: false),
                    SearchCount = table.Column<int>(nullable: false),
                    FifthCategoryID = table.Column<int>(nullable: true),
                    FourthCategoryID = table.Column<int>(nullable: true),
                    ThirdCategoryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordSecondCategory", x => x.KeywordSecondCategoryID);
                    table.ForeignKey(
                        name: "FK_KeywordSecondCategory_FifthCategory_FifthCategoryID",
                        column: x => x.FifthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FifthCategory",
                        principalColumn: "FifthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordSecondCategory_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordSecondCategory_FourthCategory_FourthCategoryID",
                        column: x => x.FourthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FourthCategory",
                        principalColumn: "FourthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordSecondCategory_SecondCategory_SecondCategoryID",
                        column: x => x.SecondCategoryID,
                        principalSchema: "cat",
                        principalTable: "SecondCategory",
                        principalColumn: "SecondCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordSecondCategory_ThirdCategory_ThirdCategoryID",
                        column: x => x.ThirdCategoryID,
                        principalSchema: "cat",
                        principalTable: "ThirdCategory",
                        principalColumn: "ThirdCategoryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "KeywordThirdCategory",
                schema: "cat",
                columns: table => new
                {
                    KeywordThirdCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    SecondCategoryID = table.Column<int>(nullable: false),
                    ThirdCategoryID = table.Column<int>(nullable: false),
                    Keyword = table.Column<string>(maxLength: 100, nullable: false),
                    URL = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 153, nullable: false),
                    Description = table.Column<string>(maxLength: 153, nullable: false),
                    SearchCount = table.Column<int>(nullable: false),
                    FifthCategoryID = table.Column<int>(nullable: true),
                    FourthCategoryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordThirdCategory", x => x.KeywordThirdCategoryID);
                    table.ForeignKey(
                        name: "FK_KeywordThirdCategory_FifthCategory_FifthCategoryID",
                        column: x => x.FifthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FifthCategory",
                        principalColumn: "FifthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordThirdCategory_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordThirdCategory_FourthCategory_FourthCategoryID",
                        column: x => x.FourthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FourthCategory",
                        principalColumn: "FourthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordThirdCategory_SecondCategory_SecondCategoryID",
                        column: x => x.SecondCategoryID,
                        principalSchema: "cat",
                        principalTable: "SecondCategory",
                        principalColumn: "SecondCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordThirdCategory_ThirdCategory_ThirdCategoryID",
                        column: x => x.ThirdCategoryID,
                        principalSchema: "cat",
                        principalTable: "ThirdCategory",
                        principalColumn: "ThirdCategoryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SixthCategory",
                schema: "cat",
                columns: table => new
                {
                    SixthCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    SecondCategoryID = table.Column<int>(nullable: false),
                    ThirdCategoryID = table.Column<int>(nullable: false),
                    FourthCategoryID = table.Column<int>(nullable: false),
                    FifthCategoryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SearchKeywordName = table.Column<string>(maxLength: 50, nullable: false),
                    URL = table.Column<string>(maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 153, nullable: false),
                    Keyword = table.Column<string>(nullable: true),
                    SearchCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SixthCategory", x => x.SixthCategoryID);
                    table.ForeignKey(
                        name: "FK_SixthCategory_FifthCategory_FifthCategoryID",
                        column: x => x.FifthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FifthCategory",
                        principalColumn: "FifthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SixthCategory_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SixthCategory_FourthCategory_FourthCategoryID",
                        column: x => x.FourthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FourthCategory",
                        principalColumn: "FourthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SixthCategory_SecondCategory_SecondCategoryID",
                        column: x => x.SecondCategoryID,
                        principalSchema: "cat",
                        principalTable: "SecondCategory",
                        principalColumn: "SecondCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SixthCategory_ThirdCategory_ThirdCategoryID",
                        column: x => x.ThirdCategoryID,
                        principalSchema: "cat",
                        principalTable: "ThirdCategory",
                        principalColumn: "ThirdCategoryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "KeywordSixthCategory",
                schema: "cat",
                columns: table => new
                {
                    KeywordSixthCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    SecondCategoryID = table.Column<int>(nullable: false),
                    ThirdCategoryID = table.Column<int>(nullable: false),
                    FourthCategoryID = table.Column<int>(nullable: false),
                    FifthCategoryID = table.Column<int>(nullable: false),
                    SixthCategoryID = table.Column<int>(nullable: false),
                    Keyword = table.Column<string>(maxLength: 100, nullable: false),
                    URL = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 153, nullable: false),
                    Description = table.Column<string>(maxLength: 153, nullable: false),
                    SearchCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordSixthCategory", x => x.KeywordSixthCategoryID);
                    table.ForeignKey(
                        name: "FK_KeywordSixthCategory_FifthCategory_FifthCategoryID",
                        column: x => x.FifthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FifthCategory",
                        principalColumn: "FifthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordSixthCategory_FirstCategory_FirstCategoryID",
                        column: x => x.FirstCategoryID,
                        principalSchema: "cat",
                        principalTable: "FirstCategory",
                        principalColumn: "FirstCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordSixthCategory_FourthCategory_FourthCategoryID",
                        column: x => x.FourthCategoryID,
                        principalSchema: "cat",
                        principalTable: "FourthCategory",
                        principalColumn: "FourthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordSixthCategory_SecondCategory_SecondCategoryID",
                        column: x => x.SecondCategoryID,
                        principalSchema: "cat",
                        principalTable: "SecondCategory",
                        principalColumn: "SecondCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordSixthCategory_SixthCategory_SixthCategoryID",
                        column: x => x.SixthCategoryID,
                        principalSchema: "cat",
                        principalTable: "SixthCategory",
                        principalColumn: "SixthCategoryID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KeywordSixthCategory_ThirdCategory_ThirdCategoryID",
                        column: x => x.ThirdCategoryID,
                        principalSchema: "cat",
                        principalTable: "ThirdCategory",
                        principalColumn: "ThirdCategoryID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandsCategory_BrandID",
                schema: "brands",
                table: "BrandsCategory",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_BrandsCategory_FirstCategoryID",
                schema: "brands",
                table: "BrandsCategory",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_BrandsCategory_SecondCategoryID",
                schema: "brands",
                table: "BrandsCategory",
                column: "SecondCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_FifthCategory_FirstCategoryID",
                schema: "cat",
                table: "FifthCategory",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_FifthCategory_FourthCategoryID",
                schema: "cat",
                table: "FifthCategory",
                column: "FourthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_FifthCategory_SecondCategoryID",
                schema: "cat",
                table: "FifthCategory",
                column: "SecondCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_FifthCategory_ThirdCategoryID",
                schema: "cat",
                table: "FifthCategory",
                column: "ThirdCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_FourthCategory_FirstCategoryID",
                schema: "cat",
                table: "FourthCategory",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_FourthCategory_SecondCategoryID",
                schema: "cat",
                table: "FourthCategory",
                column: "SecondCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_FourthCategory_ThirdCategoryID",
                schema: "cat",
                table: "FourthCategory",
                column: "ThirdCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordFifthCategory_FifthCategoryID",
                schema: "cat",
                table: "KeywordFifthCategory",
                column: "FifthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordFifthCategory_FirstCategoryID",
                schema: "cat",
                table: "KeywordFifthCategory",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordFifthCategory_FourthCategoryID",
                schema: "cat",
                table: "KeywordFifthCategory",
                column: "FourthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordFifthCategory_SecondCategoryID",
                schema: "cat",
                table: "KeywordFifthCategory",
                column: "SecondCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordFifthCategory_ThirdCategoryID",
                schema: "cat",
                table: "KeywordFifthCategory",
                column: "ThirdCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordFirstCategory_FirstCategoryID",
                schema: "cat",
                table: "KeywordFirstCategory",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordFourthCategory_FifthCategoryID",
                schema: "cat",
                table: "KeywordFourthCategory",
                column: "FifthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordFourthCategory_FirstCategoryID",
                schema: "cat",
                table: "KeywordFourthCategory",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordFourthCategory_FourthCategoryID",
                schema: "cat",
                table: "KeywordFourthCategory",
                column: "FourthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordFourthCategory_SecondCategoryID",
                schema: "cat",
                table: "KeywordFourthCategory",
                column: "SecondCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordFourthCategory_ThirdCategoryID",
                schema: "cat",
                table: "KeywordFourthCategory",
                column: "ThirdCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordSecondCategory_FifthCategoryID",
                schema: "cat",
                table: "KeywordSecondCategory",
                column: "FifthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordSecondCategory_FirstCategoryID",
                schema: "cat",
                table: "KeywordSecondCategory",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordSecondCategory_FourthCategoryID",
                schema: "cat",
                table: "KeywordSecondCategory",
                column: "FourthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordSecondCategory_SecondCategoryID",
                schema: "cat",
                table: "KeywordSecondCategory",
                column: "SecondCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordSecondCategory_ThirdCategoryID",
                schema: "cat",
                table: "KeywordSecondCategory",
                column: "ThirdCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordSixthCategory_FifthCategoryID",
                schema: "cat",
                table: "KeywordSixthCategory",
                column: "FifthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordSixthCategory_FirstCategoryID",
                schema: "cat",
                table: "KeywordSixthCategory",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordSixthCategory_FourthCategoryID",
                schema: "cat",
                table: "KeywordSixthCategory",
                column: "FourthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordSixthCategory_SecondCategoryID",
                schema: "cat",
                table: "KeywordSixthCategory",
                column: "SecondCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordSixthCategory_SixthCategoryID",
                schema: "cat",
                table: "KeywordSixthCategory",
                column: "SixthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordSixthCategory_ThirdCategoryID",
                schema: "cat",
                table: "KeywordSixthCategory",
                column: "ThirdCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordThirdCategory_FifthCategoryID",
                schema: "cat",
                table: "KeywordThirdCategory",
                column: "FifthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordThirdCategory_FirstCategoryID",
                schema: "cat",
                table: "KeywordThirdCategory",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordThirdCategory_FourthCategoryID",
                schema: "cat",
                table: "KeywordThirdCategory",
                column: "FourthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordThirdCategory_SecondCategoryID",
                schema: "cat",
                table: "KeywordThirdCategory",
                column: "SecondCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordThirdCategory_ThirdCategoryID",
                schema: "cat",
                table: "KeywordThirdCategory",
                column: "ThirdCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SecondCategory_FirstCategoryID",
                schema: "cat",
                table: "SecondCategory",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SixthCategory_FifthCategoryID",
                schema: "cat",
                table: "SixthCategory",
                column: "FifthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SixthCategory_FirstCategoryID",
                schema: "cat",
                table: "SixthCategory",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SixthCategory_FourthCategoryID",
                schema: "cat",
                table: "SixthCategory",
                column: "FourthCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SixthCategory_SecondCategoryID",
                schema: "cat",
                table: "SixthCategory",
                column: "SecondCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SixthCategory_ThirdCategoryID",
                schema: "cat",
                table: "SixthCategory",
                column: "ThirdCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdCategory_FirstCategoryID",
                schema: "cat",
                table: "ThirdCategory",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdCategory_SecondCategoryID",
                schema: "cat",
                table: "ThirdCategory",
                column: "SecondCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandsCategory",
                schema: "brands");

            migrationBuilder.DropTable(
                name: "KeywordBrand",
                schema: "brands");

            migrationBuilder.DropTable(
                name: "KeywordFifthCategory",
                schema: "cat");

            migrationBuilder.DropTable(
                name: "KeywordFirstCategory",
                schema: "cat");

            migrationBuilder.DropTable(
                name: "KeywordFourthCategory",
                schema: "cat");

            migrationBuilder.DropTable(
                name: "KeywordSecondCategory",
                schema: "cat");

            migrationBuilder.DropTable(
                name: "KeywordSixthCategory",
                schema: "cat");

            migrationBuilder.DropTable(
                name: "KeywordThirdCategory",
                schema: "cat");

            migrationBuilder.DropTable(
                name: "NewSearchTerm",
                schema: "cat");

            migrationBuilder.DropTable(
                name: "Pages",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "Brand",
                schema: "brands");

            migrationBuilder.DropTable(
                name: "SixthCategory",
                schema: "cat");

            migrationBuilder.DropTable(
                name: "FifthCategory",
                schema: "cat");

            migrationBuilder.DropTable(
                name: "FourthCategory",
                schema: "cat");

            migrationBuilder.DropTable(
                name: "ThirdCategory",
                schema: "cat");

            migrationBuilder.DropTable(
                name: "SecondCategory",
                schema: "cat");

            migrationBuilder.DropTable(
                name: "FirstCategory",
                schema: "cat");
        }
    }
}
