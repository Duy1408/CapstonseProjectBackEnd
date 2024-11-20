using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v50 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitType_Project_ProjectID",
                table: "UnitType");

            migrationBuilder.DropIndex(
                name: "IX_UnitType_ProjectID",
                table: "UnitType");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "UnitType");

            migrationBuilder.AddColumn<string>(
                name: "ContractTransferFile",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractTransferFile",
                table: "Contract");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "UnitType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UnitType_ProjectID",
                table: "UnitType",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitType_Project_ProjectID",
                table: "UnitType",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID");
        }
    }
}
