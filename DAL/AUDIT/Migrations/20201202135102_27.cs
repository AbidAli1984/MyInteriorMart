using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingNotification_EntityType_EntityTypeID",
                schema: "notif",
                table: "ListingNotification");

            migrationBuilder.DropTable(
                name: "EntityType",
                schema: "notif");

            migrationBuilder.DropIndex(
                name: "IX_ListingNotification_EntityTypeID",
                schema: "notif",
                table: "ListingNotification");

            migrationBuilder.DropColumn(
                name: "EntityTypeID",
                schema: "notif",
                table: "ListingNotification");

            migrationBuilder.AddColumn<string>(
                name: "EntityType",
                schema: "notif",
                table: "ListingNotification",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityType",
                schema: "notif",
                table: "ListingNotification");

            migrationBuilder.AddColumn<int>(
                name: "EntityTypeID",
                schema: "notif",
                table: "ListingNotification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EntityType",
                schema: "notif",
                columns: table => new
                {
                    EntityTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityType", x => x.EntityTypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingNotification_EntityTypeID",
                schema: "notif",
                table: "ListingNotification",
                column: "EntityTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ListingNotification_EntityType_EntityTypeID",
                schema: "notif",
                table: "ListingNotification",
                column: "EntityTypeID",
                principalSchema: "notif",
                principalTable: "EntityType",
                principalColumn: "EntityTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
