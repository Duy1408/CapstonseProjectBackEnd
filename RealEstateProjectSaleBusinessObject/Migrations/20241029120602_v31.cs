using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_PaymentType_PaymentTypeID",
                table: "Payment");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropIndex(
                name: "IX_Payment_PaymentTypeID",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "PaymentTypeID",
                table: "Payment");

            migrationBuilder.AddColumn<string>(
                name: "DeviceToken",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceToken",
                table: "Customer");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentTypeID",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    PaymentTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.PaymentTypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentTypeID",
                table: "Payment",
                column: "PaymentTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PaymentType_PaymentTypeID",
                table: "Payment",
                column: "PaymentTypeID",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeID");
        }
    }
}
