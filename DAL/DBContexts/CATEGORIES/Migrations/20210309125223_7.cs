using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.CATEGORIES.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "cat",
                table: "ThirdCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "ThirdCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "cat",
                table: "ThirdCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "cat",
                table: "SixthCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "SixthCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "cat",
                table: "SixthCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "cat",
                table: "SecondCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "SecondCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "cat",
                table: "SecondCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "cat",
                table: "FourthCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "FourthCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "cat",
                table: "FourthCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "cat",
                table: "FirstCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "FirstCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "cat",
                table: "FirstCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "cat",
                table: "FifthCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "FifthCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "cat",
                table: "FifthCategory",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "cat",
                table: "ThirdCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "ThirdCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "cat",
                table: "ThirdCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "cat",
                table: "SixthCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "SixthCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "cat",
                table: "SixthCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "cat",
                table: "SecondCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "SecondCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "cat",
                table: "SecondCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "cat",
                table: "FourthCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "FourthCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "cat",
                table: "FourthCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "cat",
                table: "FirstCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "FirstCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "cat",
                table: "FirstCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                schema: "cat",
                table: "FifthCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "SearchKeywordName",
                schema: "cat",
                table: "FifthCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "cat",
                table: "FifthCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);
        }
    }
}
