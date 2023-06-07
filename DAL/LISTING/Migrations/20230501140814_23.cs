using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.LISTING.Migrations
{
    public partial class _23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lab");

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "lab",
                columns: table => new
                {
                    ContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(nullable: false),
                    PermanentAddress = table.Column<string>(nullable: false),
                    NativeAddress = table.Column<string>(nullable: false),
                    WhatsAppMobile = table.Column<string>(nullable: false),
                    AlternateMobile = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "lab",
                columns: table => new
                {
                    DocumentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(nullable: false),
                    AadharNumber = table.Column<string>(nullable: true),
                    PanNumber = table.Column<string>(nullable: true),
                    AadharCard = table.Column<bool>(nullable: false),
                    PanCard = table.Column<bool>(nullable: false),
                    VoterId = table.Column<bool>(nullable: false),
                    DrivingLicense = table.Column<bool>(nullable: false),
                    ElectricityBill = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.DocumentId);
                });

            migrationBuilder.CreateTable(
                name: "LaborCategory",
                schema: "lab",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    MetaTitle = table.Column<string>(nullable: false),
                    MetaDescription = table.Column<string>(nullable: false),
                    IsChild = table.Column<bool>(nullable: false),
                    ParentCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                schema: "lab",
                columns: table => new
                {
                    PersonalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    Married = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.PersonalId);
                });

            migrationBuilder.CreateTable(
                name: "Profession",
                schema: "lab",
                columns: table => new
                {
                    ProfessionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(nullable: false),
                    Qualification = table.Column<string>(nullable: false),
                    English = table.Column<bool>(nullable: false),
                    Hindi = table.Column<bool>(nullable: false),
                    Marathi = table.Column<bool>(nullable: false),
                    Gujrati = table.Column<bool>(nullable: false),
                    Urdu = table.Column<bool>(nullable: false),
                    Tamil = table.Column<bool>(nullable: false),
                    Kannada = table.Column<bool>(nullable: false),
                    Telegu = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profession", x => x.ProfessionId);
                });

            migrationBuilder.CreateTable(
                name: "Reference",
                schema: "lab",
                columns: table => new
                {
                    ReferenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(nullable: false),
                    Ref1stName = table.Column<string>(nullable: false),
                    Ref1stMobile = table.Column<string>(nullable: false),
                    Ref1stAddress = table.Column<string>(nullable: false),
                    Ref1stRelationship = table.Column<string>(nullable: false),
                    Ref2ndName = table.Column<string>(nullable: false),
                    Ref2ndMobile = table.Column<string>(nullable: false),
                    Ref2ndAddress = table.Column<string>(nullable: false),
                    Ref2ndRelationship = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reference", x => x.ReferenceId);
                });

            migrationBuilder.CreateTable(
                name: "Classification",
                schema: "lab",
                columns: table => new
                {
                    ClassificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(nullable: false),
                    MistryORLabour = table.Column<string>(nullable: true),
                    ParentCategoryId = table.Column<int>(nullable: false),
                    ChildCategoryId = table.Column<int>(nullable: false),
                    LaborCategoryCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.ClassificationId);
                    table.ForeignKey(
                        name: "FK_Classification_LaborCategory_LaborCategoryCategoryId",
                        column: x => x.LaborCategoryCategoryId,
                        principalSchema: "lab",
                        principalTable: "LaborCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classification_LaborCategoryCategoryId",
                schema: "lab",
                table: "Classification",
                column: "LaborCategoryCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classification",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "Personal",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "Profession",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "Reference",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "LaborCategory",
                schema: "lab");
        }
    }
}
