using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectID",
                table: "PaymentPolicy",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.DropPrimaryKey("PK_PaymentPolicy", "PaymentPolicy");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentPolicyID",
                table: "PaymentPolicy",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey("PK_PaymentPolicy", "PaymentPolicy", "PaymentPolicyID");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentPolicyID",
                table: "ContractPaymentDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerID",
                table: "Contract",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionDetaiID",
                table: "Contract",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionDetailPromotionDetaiID",
                table: "Contract",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractPaymentDetail_PaymentPolicyID",
                table: "ContractPaymentDetail",
                column: "PaymentPolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CustomerID",
                table: "Contract",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_PromotionDetailPromotionDetaiID",
                table: "Contract",
                column: "PromotionDetailPromotionDetaiID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Customer_CustomerID",
                table: "Contract",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_PromotionDetail_PromotionDetailPromotionDetaiID",
                table: "Contract",
                column: "PromotionDetailPromotionDetaiID",
                principalTable: "PromotionDetail",
                principalColumn: "PromotionDetaiID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractPaymentDetail_PaymentPolicy_PaymentPolicyID",
                table: "ContractPaymentDetail",
                column: "PaymentPolicyID",
                principalTable: "PaymentPolicy",
                principalColumn: "PaymentPolicyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Customer_CustomerID",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_PromotionDetail_PromotionDetailPromotionDetaiID",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractPaymentDetail_PaymentPolicy_PaymentPolicyID",
                table: "ContractPaymentDetail");

            migrationBuilder.DropIndex(
                name: "IX_ContractPaymentDetail_PaymentPolicyID",
                table: "ContractPaymentDetail");

            migrationBuilder.DropIndex(
                name: "IX_Contract_CustomerID",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_PromotionDetailPromotionDetaiID",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "PaymentPolicyID",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "PromotionDetaiID",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "PromotionDetailPromotionDetaiID",
                table: "Contract");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectID",
                table: "PaymentPolicy",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentPolicyID",
                table: "PaymentPolicy",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
