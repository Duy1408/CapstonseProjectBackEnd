using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectCategoryDetailProjectID",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectCategoryDetailPropertyCategoryID",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "Property",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyCategoryID",
                table: "Property",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyCategoryID",
                table: "OpeningForSale",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Property_ProjectCategoryDetailProjectID_ProjectCategoryDetailPropertyCategoryID",
                table: "Property",
                columns: new[] { "ProjectCategoryDetailProjectID", "ProjectCategoryDetailPropertyCategoryID" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Property_ProjectCategoryDetail_ProjectCategoryDetailProjectID_ProjectCategoryDetailPropertyCategoryID",
                table: "Property",
                columns: new[] { "ProjectCategoryDetailProjectID", "ProjectCategoryDetailPropertyCategoryID" },
                principalTable: "ProjectCategoryDetail",
                principalColumns: new[] { "ProjectID", "PropertyCategoryID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningForSale_PropertyCategory_PropertyCategoryID",
                table: "OpeningForSale");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_ProjectCategoryDetail_ProjectCategoryDetailProjectID_ProjectCategoryDetailPropertyCategoryID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_ProjectCategoryDetailProjectID_ProjectCategoryDetailPropertyCategoryID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_OpeningForSale_PropertyCategoryID",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "ProjectCategoryDetailProjectID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "ProjectCategoryDetailPropertyCategoryID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "PropertyCategoryID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "PropertyCategoryID",
                table: "OpeningForSale");
        }
    }
}
