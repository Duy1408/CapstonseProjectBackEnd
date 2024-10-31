using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Project_ProjectID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_PropertyCategory_PropertyCategoryID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningForSale_Project_ProjectID",
                table: "OpeningForSale");

            migrationBuilder.DropIndex(
                name: "IX_OpeningForSale_ProjectID",
                table: "OpeningForSale");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ProjectID",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_PropertyCategoryID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "PropertyCategoryID",
                table: "Booking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "OpeningForSale",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyCategoryID",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OpeningForSale_ProjectID",
                table: "OpeningForSale",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ProjectID",
                table: "Booking",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_PropertyCategoryID",
                table: "Booking",
                column: "PropertyCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Project_ProjectID",
                table: "Booking",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_PropertyCategory_PropertyCategoryID",
                table: "Booking",
                column: "PropertyCategoryID",
                principalTable: "PropertyCategory",
                principalColumn: "PropertyCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningForSale_Project_ProjectID",
                table: "OpeningForSale",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID");
        }
    }
}
