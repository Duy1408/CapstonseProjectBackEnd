using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_ProjectCategoryDetail_ProjectCategoryDetailProjectID_ProjectCategoryDetailPropertyCategoryID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_ProjectCategoryDetailProjectID_ProjectCategoryDetailPropertyCategoryID",
                table: "Property");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectCategoryDetail",
                table: "ProjectCategoryDetail");

            migrationBuilder.DropColumn(
                name: "ProjectCategoryDetailProjectID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "ProjectCategoryDetailPropertyCategoryID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "PropertyCategoryID",
                table: "Property",
                newName: "ProjectCategoryDetailID");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectCategoryDetailID",
                table: "ProjectCategoryDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectCategoryDetail",
                table: "ProjectCategoryDetail",
                column: "ProjectCategoryDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ProjectCategoryDetailID",
                table: "Property",
                column: "ProjectCategoryDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategoryDetail_ProjectID",
                table: "ProjectCategoryDetail",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_ProjectCategoryDetail_ProjectCategoryDetailID",
                table: "Property",
                column: "ProjectCategoryDetailID",
                principalTable: "ProjectCategoryDetail",
                principalColumn: "ProjectCategoryDetailID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_ProjectCategoryDetail_ProjectCategoryDetailID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_ProjectCategoryDetailID",
                table: "Property");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectCategoryDetail",
                table: "ProjectCategoryDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectCategoryDetail_ProjectID",
                table: "ProjectCategoryDetail");

            migrationBuilder.DropColumn(
                name: "ProjectCategoryDetailID",
                table: "ProjectCategoryDetail");

            migrationBuilder.RenameColumn(
                name: "ProjectCategoryDetailID",
                table: "Property",
                newName: "PropertyCategoryID");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectCategoryDetail",
                table: "ProjectCategoryDetail",
                columns: new[] { "ProjectID", "PropertyCategoryID" });

            migrationBuilder.CreateIndex(
                name: "IX_Property_ProjectCategoryDetailProjectID_ProjectCategoryDetailPropertyCategoryID",
                table: "Property",
                columns: new[] { "ProjectCategoryDetailProjectID", "ProjectCategoryDetailPropertyCategoryID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Property_ProjectCategoryDetail_ProjectCategoryDetailProjectID_ProjectCategoryDetailPropertyCategoryID",
                table: "Property",
                columns: new[] { "ProjectCategoryDetailProjectID", "ProjectCategoryDetailPropertyCategoryID" },
                principalTable: "ProjectCategoryDetail",
                principalColumns: new[] { "ProjectID", "PropertyCategoryID" });
        }
    }
}
