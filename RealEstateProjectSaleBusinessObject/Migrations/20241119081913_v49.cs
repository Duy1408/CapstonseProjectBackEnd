using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v49 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerID",
                table: "Notification",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CustomerID",
                table: "Notification",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Customer_CustomerID",
                table: "Notification",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Customer_CustomerID",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_CustomerID",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Notification");
        }
    }
}
