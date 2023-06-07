using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.LISTING.Migrations
{
    public partial class _25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryBanner",
                schema: "ban",
                columns: table => new
                {
                    BannerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placement = table.Column<string>(nullable: false),
                    FirstCategoryID = table.Column<int>(nullable: false),
                    SecondCategoryID = table.Column<int>(nullable: false),
                    ThirdCategoryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    LinkUrl = table.Column<string>(nullable: false),
                    TargetWindow = table.Column<string>(nullable: false),
                    Disable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryBanner", x => x.BannerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryBanner",
                schema: "ban");
        }
    }
}
