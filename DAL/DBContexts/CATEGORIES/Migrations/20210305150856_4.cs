using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.CATEGORIES.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandsCategory_FirstCategory_FirstCategoryID",
                schema: "brands",
                table: "BrandsCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandsCategory_SecondCategory_SecondCategoryID",
                schema: "brands",
                table: "BrandsCategory");

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

            migrationBuilder.DropColumn(
                name: "Descript",
                schema: "brands",
                table: "Brand");

            migrationBuilder.AlterColumn<int>(
                name: "SecondCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Keyword",
                schema: "brands",
                table: "KeywordBrand",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FirstCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BrandCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SecondCategoryID",
                schema: "brands",
                table: "BrandsCategory",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FirstCategoryID",
                schema: "brands",
                table: "BrandsCategory",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandCategoryName",
                schema: "brands",
                table: "BrandsCategory",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "brands",
                table: "Brand",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "brands",
                table: "Brand",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "brands",
                table: "Brand",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandsCategory_FirstCategory_FirstCategoryID",
                schema: "brands",
                table: "BrandsCategory",
                column: "FirstCategoryID",
                principalSchema: "cat",
                principalTable: "FirstCategory",
                principalColumn: "FirstCategoryID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandsCategory_SecondCategory_SecondCategoryID",
                schema: "brands",
                table: "BrandsCategory",
                column: "SecondCategoryID",
                principalSchema: "cat",
                principalTable: "SecondCategory",
                principalColumn: "SecondCategoryID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordBrand_BrandsCategory_BrandCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                column: "BrandCategoryID",
                principalSchema: "brands",
                principalTable: "BrandsCategory",
                principalColumn: "BrandCategoryID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordBrand_FirstCategory_FirstCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                column: "FirstCategoryID",
                principalSchema: "cat",
                principalTable: "FirstCategory",
                principalColumn: "FirstCategoryID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordBrand_SecondCategory_SecondCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                column: "SecondCategoryID",
                principalSchema: "cat",
                principalTable: "SecondCategory",
                principalColumn: "SecondCategoryID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandsCategory_FirstCategory_FirstCategoryID",
                schema: "brands",
                table: "BrandsCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandsCategory_SecondCategory_SecondCategoryID",
                schema: "brands",
                table: "BrandsCategory");

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

            migrationBuilder.DropColumn(
                name: "BrandCategoryName",
                schema: "brands",
                table: "BrandsCategory");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "brands",
                table: "Brand");

            migrationBuilder.AlterColumn<int>(
                name: "SecondCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Keyword",
                schema: "brands",
                table: "KeywordBrand",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "FirstCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BrandCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SecondCategoryID",
                schema: "brands",
                table: "BrandsCategory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FirstCategoryID",
                schema: "brands",
                table: "BrandsCategory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "brands",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "brands",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Descript",
                schema: "brands",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandsCategory_FirstCategory_FirstCategoryID",
                schema: "brands",
                table: "BrandsCategory",
                column: "FirstCategoryID",
                principalSchema: "cat",
                principalTable: "FirstCategory",
                principalColumn: "FirstCategoryID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandsCategory_SecondCategory_SecondCategoryID",
                schema: "brands",
                table: "BrandsCategory",
                column: "SecondCategoryID",
                principalSchema: "cat",
                principalTable: "SecondCategory",
                principalColumn: "SecondCategoryID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordBrand_BrandsCategory_BrandCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                column: "BrandCategoryID",
                principalSchema: "brands",
                principalTable: "BrandsCategory",
                principalColumn: "BrandCategoryID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordBrand_FirstCategory_FirstCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                column: "FirstCategoryID",
                principalSchema: "cat",
                principalTable: "FirstCategory",
                principalColumn: "FirstCategoryID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordBrand_SecondCategory_SecondCategoryID",
                schema: "brands",
                table: "KeywordBrand",
                column: "SecondCategoryID",
                principalSchema: "cat",
                principalTable: "SecondCategory",
                principalColumn: "SecondCategoryID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
