using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.SHARED.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                schema: "shared",
                columns: table => new
                {
                    MessageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    SmsMessage = table.Column<string>(maxLength: 160, nullable: false),
                    EmailSubject = table.Column<string>(maxLength: 750, nullable: false),
                    EmailMessage = table.Column<string>(nullable: false),
                    Variables = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages",
                schema: "shared");
        }
    }
}
