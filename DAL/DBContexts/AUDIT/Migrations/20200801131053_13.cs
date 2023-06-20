using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscribes",
                schema: "audit",
                columns: table => new
                {
                    SubscribeID = table.Column<int>(nullable: false)
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
                    Subscribe = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribes", x => x.SubscribeID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscribes",
                schema: "audit");
        }
    }
}
