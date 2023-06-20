using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.BILLING.Migrations
{
    public partial class _003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptedTermsConditions",
                schema: "plan",
                table: "Subscription",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                schema: "plan",
                table: "Subscription",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                schema: "plan",
                table: "Subscription",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "OrderAmount",
                schema: "plan",
                table: "Subscription",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                schema: "plan",
                table: "Subscription",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazorpayOrderID",
                schema: "plan",
                table: "Subscription",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazorpayPaymentID",
                schema: "plan",
                table: "Subscription",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazorpaySignature",
                schema: "plan",
                table: "Subscription",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedTermsConditions",
                schema: "plan",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "CouponCode",
                schema: "plan",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                schema: "plan",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "OrderAmount",
                schema: "plan",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                schema: "plan",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "RazorpayOrderID",
                schema: "plan",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "RazorpayPaymentID",
                schema: "plan",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "RazorpaySignature",
                schema: "plan",
                table: "Subscription");
        }
    }
}
