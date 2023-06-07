using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.AUDIT.Migrations
{
    public partial class _23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "notif");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UsersOnline",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "EntityType",
                schema: "notif",
                columns: table => new
                {
                    EntityTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityType", x => x.EntityTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ListingNotification",
                schema: "notif",
                columns: table => new
                {
                    ListingNotificationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    ActorGUID = table.Column<string>(nullable: false),
                    NotifierGUID = table.Column<string>(nullable: false),
                    EntityTypeID = table.Column<int>(nullable: false),
                    EntityID = table.Column<int>(nullable: false),
                    Action = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingNotification", x => x.ListingNotificationID);
                    table.ForeignKey(
                        name: "FK_ListingNotification_EntityType_EntityTypeID",
                        column: x => x.EntityTypeID,
                        principalSchema: "notif",
                        principalTable: "EntityType",
                        principalColumn: "EntityTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingNotification_EntityTypeID",
                schema: "notif",
                table: "ListingNotification",
                column: "EntityTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingNotification",
                schema: "notif");

            migrationBuilder.DropTable(
                name: "EntityType",
                schema: "notif");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UsersOnline",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
