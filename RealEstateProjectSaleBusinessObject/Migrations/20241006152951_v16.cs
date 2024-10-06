using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Booking_BookingID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_DocumentTemplate_DocumentTemplateID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_PaymentProcess_PaymentProcessID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractPaymentDetail_Contact_ContractID",
                table: "ContractPaymentDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.RenameTable(
                name: "Contact",
                newName: "Contract");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_PaymentProcessID",
                table: "Contract",
                newName: "IX_Contract_PaymentProcessID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_DocumentTemplateID",
                table: "Contract",
                newName: "IX_Contract_DocumentTemplateID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_BookingID",
                table: "Contract",
                newName: "IX_Contract_BookingID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract",
                table: "Contract",
                column: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Booking_BookingID",
                table: "Contract",
                column: "BookingID",
                principalTable: "Booking",
                principalColumn: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_DocumentTemplate_DocumentTemplateID",
                table: "Contract",
                column: "DocumentTemplateID",
                principalTable: "DocumentTemplate",
                principalColumn: "DocumentTemplateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_PaymentProcess_PaymentProcessID",
                table: "Contract",
                column: "PaymentProcessID",
                principalTable: "PaymentProcess",
                principalColumn: "PaymentProcessID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractPaymentDetail_Contract_ContractID",
                table: "ContractPaymentDetail",
                column: "ContractID",
                principalTable: "Contract",
                principalColumn: "ContractID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Booking_BookingID",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_DocumentTemplate_DocumentTemplateID",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_PaymentProcess_PaymentProcessID",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractPaymentDetail_Contract_ContractID",
                table: "ContractPaymentDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract",
                table: "Contract");

            migrationBuilder.RenameTable(
                name: "Contract",
                newName: "Contact");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_PaymentProcessID",
                table: "Contact",
                newName: "IX_Contact_PaymentProcessID");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_DocumentTemplateID",
                table: "Contact",
                newName: "IX_Contact_DocumentTemplateID");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_BookingID",
                table: "Contact",
                newName: "IX_Contact_BookingID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Booking_BookingID",
                table: "Contact",
                column: "BookingID",
                principalTable: "Booking",
                principalColumn: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_DocumentTemplate_DocumentTemplateID",
                table: "Contact",
                column: "DocumentTemplateID",
                principalTable: "DocumentTemplate",
                principalColumn: "DocumentTemplateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_PaymentProcess_PaymentProcessID",
                table: "Contact",
                column: "PaymentProcessID",
                principalTable: "PaymentProcess",
                principalColumn: "PaymentProcessID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractPaymentDetail_Contact_ContractID",
                table: "ContractPaymentDetail",
                column: "ContractID",
                principalTable: "Contact",
                principalColumn: "ContractID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
