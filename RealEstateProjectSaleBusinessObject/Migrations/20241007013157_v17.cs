using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCategory_Project_ProjectID",
                table: "PropertyCategory");

            migrationBuilder.DropIndex(
                name: "IX_PropertyCategory_ProjectID",
                table: "PropertyCategory");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "PropertyCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "PropertyCategory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PropertyCategory_ProjectID",
                table: "PropertyCategory",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyCategory_Project_ProjectID",
                table: "PropertyCategory",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID");
        }
    }
}
