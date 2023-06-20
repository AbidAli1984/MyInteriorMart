using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.BILLING.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bill");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "bill",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<string>(nullable: false),
                    SubscriptionID = table.Column<int>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    RazorpayOrderID = table.Column<string>(nullable: true),
                    RazorpayPaymentID = table.Column<string>(nullable: true),
                    RazorpaySignature = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<string>(nullable: true),
                    CouponCode = table.Column<string>(nullable: true),
                    PaymentMethod = table.Column<string>(nullable: true),
                    OrderAmount = table.Column<decimal>(nullable: false),
                    AcceptedTermsConditions = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Subscription_SubscriptionID",
                        column: x => x.SubscriptionID,
                        principalSchema: "plan",
                        principalTable: "Subscription",
                        principalColumn: "SubscriptionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SubscriptionID",
                schema: "bill",
                table: "Orders",
                column: "SubscriptionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders",
                schema: "bill");
        }
    }
}
