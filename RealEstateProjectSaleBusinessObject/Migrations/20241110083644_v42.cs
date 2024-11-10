using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v42 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amountpaid",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "DetailName",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "InterestRate",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "MoneyExist",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "MoneyInterestRate",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "NumberDayLate",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "ContractName",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "DateSigned",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "MoneyTax",
                table: "ContractPaymentDetail",
                newName: "PaidValueLate");

            migrationBuilder.RenameColumn(
                name: "MoneyReceived",
                table: "ContractPaymentDetail",
                newName: "PaidValue");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PaymentProcessDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Period",
                table: "ContractPaymentDetail",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "ContractPaymentDetail",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PaymentProcessDetail");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ContractPaymentDetail");

            migrationBuilder.RenameColumn(
                name: "PaidValueLate",
                table: "ContractPaymentDetail",
                newName: "MoneyTax");

            migrationBuilder.RenameColumn(
                name: "PaidValue",
                table: "ContractPaymentDetail",
                newName: "MoneyReceived");

            migrationBuilder.AddColumn<double>(
                name: "Amountpaid",
                table: "ContractPaymentDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "ContractPaymentDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DetailName",
                table: "ContractPaymentDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "InterestRate",
                table: "ContractPaymentDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MoneyExist",
                table: "ContractPaymentDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MoneyInterestRate",
                table: "ContractPaymentDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberDayLate",
                table: "ContractPaymentDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaxRate",
                table: "ContractPaymentDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractName",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSigned",
                table: "Contract",
                type: "date",
                nullable: true);
        }
    }
}
