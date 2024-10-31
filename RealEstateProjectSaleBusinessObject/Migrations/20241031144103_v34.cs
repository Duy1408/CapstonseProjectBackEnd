using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningForSale_PropertyCategory_PropertyCategoryID",
                table: "OpeningForSale");

            migrationBuilder.DropIndex(
                name: "IX_OpeningForSale_PropertyCategoryID",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "PropertyCategoryID",
                table: "OpeningForSale");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectCategoryDetailID",
                table: "OpeningForSale",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectCategoryDetailID",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OpeningForSale_ProjectCategoryDetailID",
                table: "OpeningForSale",
                column: "ProjectCategoryDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ProjectCategoryDetailID",
                table: "Booking",
                column: "ProjectCategoryDetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_ProjectCategoryDetail_ProjectCategoryDetailID",
                table: "Booking",
                column: "ProjectCategoryDetailID",
                principalTable: "ProjectCategoryDetail",
                principalColumn: "ProjectCategoryDetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningForSale_ProjectCategoryDetail_ProjectCategoryDetailID",
                table: "OpeningForSale",
                column: "ProjectCategoryDetailID",
                principalTable: "ProjectCategoryDetail",
                principalColumn: "ProjectCategoryDetailID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_ProjectCategoryDetail_ProjectCategoryDetailID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningForSale_ProjectCategoryDetail_ProjectCategoryDetailID",
                table: "OpeningForSale");

            migrationBuilder.DropIndex(
                name: "IX_OpeningForSale_ProjectCategoryDetailID",
                table: "OpeningForSale");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ProjectCategoryDetailID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ProjectCategoryDetailID",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "ProjectCategoryDetailID",
                table: "Booking");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyCategoryID",
                table: "OpeningForSale",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpeningForSale_PropertyCategoryID",
                table: "OpeningForSale",
                column: "PropertyCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningForSale_PropertyCategory_PropertyCategoryID",
                table: "OpeningForSale",
                column: "PropertyCategoryID",
                principalTable: "PropertyCategory",
                principalColumn: "PropertyCategoryID");
        }
    }
}
