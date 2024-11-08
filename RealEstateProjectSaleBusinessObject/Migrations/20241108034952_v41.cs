using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v41 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_PromotionDetail_PromotionDetailPromotionDetaiID",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "PromotionDetaiID",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "PromotionDetaiID",
                table: "PromotionDetail",
                newName: "PromotionDetailID");

            migrationBuilder.RenameColumn(
                name: "PromotionDetailPromotionDetaiID",
                table: "Contract",
                newName: "PromotionDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_PromotionDetailPromotionDetaiID",
                table: "Contract",
                newName: "IX_Contract_PromotionDetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_PromotionDetail_PromotionDetailID",
                table: "Contract",
                column: "PromotionDetailID",
                principalTable: "PromotionDetail",
                principalColumn: "PromotionDetailID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_PromotionDetail_PromotionDetailID",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "PromotionDetailID",
                table: "PromotionDetail",
                newName: "PromotionDetaiID");

            migrationBuilder.RenameColumn(
                name: "PromotionDetailID",
                table: "Contract",
                newName: "PromotionDetailPromotionDetaiID");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_PromotionDetailID",
                table: "Contract",
                newName: "IX_Contract_PromotionDetailPromotionDetaiID");

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionDetaiID",
                table: "Contract",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_PromotionDetail_PromotionDetailPromotionDetaiID",
                table: "Contract",
                column: "PromotionDetailPromotionDetaiID",
                principalTable: "PromotionDetail",
                principalColumn: "PromotionDetaiID");
        }
    }
}
