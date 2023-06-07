using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.BANNER.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slideshow",
                columns: table => new
                {
                    SlideID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AltAttribute = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 1000, nullable: false),
                    TargetURL = table.Column<string>(nullable: false),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slideshow", x => x.SlideID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slideshow");
        }
    }
}
