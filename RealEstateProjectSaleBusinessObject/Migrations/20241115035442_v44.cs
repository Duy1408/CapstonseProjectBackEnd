using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v44 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Customer_CustomerID",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_OpeningForSale_OpeningForSaleID",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_CustomerID",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_OpeningForSaleID",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "OpeningForSaleID",
                table: "Notification");

            migrationBuilder.AddColumn<Guid>(
                name: "BookingID",
                table: "Notification",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notification_BookingID",
                table: "Notification",
                column: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Booking_BookingID",
                table: "Notification",
                column: "BookingID",
                principalTable: "Booking",
                principalColumn: "BookingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Booking_BookingID",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_BookingID",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "BookingID",
                table: "Notification");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerID",
                table: "Notification",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OpeningForSaleID",
                table: "Notification",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CustomerID",
                table: "Notification",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_OpeningForSaleID",
                table: "Notification",
                column: "OpeningForSaleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Customer_CustomerID",
                table: "Notification",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_OpeningForSale_OpeningForSaleID",
                table: "Notification",
                column: "OpeningForSaleID",
                principalTable: "OpeningForSale",
                principalColumn: "OpeningForSaleID");
        }
    }
}
