using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.CATEGORIES.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KeywordBrand",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.DropColumn(
                name: "KeywordBrandID",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.AddColumn<int>(
                name: "BrandKeywordID",
                schema: "brands",
                table: "KeywordBrand",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BrandCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirstCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KeywordBrand",
                schema: "brands",
                table: "KeywordBrand",
                column: "BrandKeywordID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordBrand_BrandCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                column: "BrandCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordBrand_FirstCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                column: "FirstCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordBrand_SecondCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                column: "SecondCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordBrand_BrandsCategory_BrandCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                column: "BrandCategoryID",
                principalSchema: "brands",
                principalTable: "BrandsCategory",
                principalColumn: "BrandCategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordBrand_FirstCategory_FirstCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                column: "FirstCategoryID",
                principalSchema: "cat",
                principalTable: "FirstCategory",
                principalColumn: "FirstCategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordBrand_SecondCategory_SecondCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                column: "SecondCategoryID",
                principalSchema: "cat",
                principalTable: "SecondCategory",
                principalColumn: "SecondCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeywordBrand_BrandsCategory_BrandCategoryID",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.DropForeignKey(
                name: "FK_KeywordBrand_FirstCategory_FirstCategoryID",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.DropForeignKey(
                name: "FK_KeywordBrand_SecondCategory_SecondCategoryID",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KeywordBrand",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.DropIndex(
                name: "IX_KeywordBrand_BrandCategoryID",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.DropIndex(
                name: "IX_KeywordBrand_FirstCategoryID",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.DropIndex(
                name: "IX_KeywordBrand_SecondCategoryID",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.DropColumn(
                name: "BrandKeywordID",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.DropColumn(
                name: "BrandCategoryID",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.DropColumn(
                name: "FirstCategoryID",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.DropColumn(
                name: "SecondCategoryID",
                schema: "brands",
                table: "KeywordBrand");

            migrationBuilder.AddColumn<int>(
                name: "KeywordBrandID",
                schema: "brands",
                table: "KeywordBrand",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KeywordBrand",
                schema: "brands",
                table: "KeywordBrand",
                column: "KeywordBrandID");
        }
    }
}
