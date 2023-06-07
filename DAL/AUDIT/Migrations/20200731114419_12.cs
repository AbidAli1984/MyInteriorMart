using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookmarks",
                schema: "audit",
                columns: table => new
                {
                    BookmarksID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    UserGuid = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    IPAddress = table.Column<string>(nullable: true),
                    UserRole = table.Column<string>(nullable: true),
                    VisitDate = table.Column<string>(nullable: true),
                    VisitTime = table.Column<string>(nullable: true),
                    UserAgent = table.Column<string>(nullable: true),
                    Bookmark = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmarks", x => x.BookmarksID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookmarks",
                schema: "audit");
        }
    }
}
