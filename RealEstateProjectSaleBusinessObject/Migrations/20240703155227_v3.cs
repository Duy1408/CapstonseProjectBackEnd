using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRange",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "Maintenancecosts",
                table: "PaymentProcessDetail");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "PaymentProcess");

            migrationBuilder.DropColumn(
                name: "PaymentRate",
                table: "PaymentProcess");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Deposittoholdproject",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "DepositMoney",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "Deposittime",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "Explain",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "MoneyType",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "RevervationMoney",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "OpenForSaleDetail");

            migrationBuilder.DropColumn(
                name: "TypeRoom",
                table: "OpenForSaleDetail");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PropertyType",
                newName: "TypeName");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Property",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "Paymentprocessname",
                table: "PaymentProcessDetail",
                newName: "DetailName");

            migrationBuilder.RenameColumn(
                name: "Paymentperiod",
                table: "PaymentProcess",
                newName: "PaymentProcessName");

            migrationBuilder.RenameColumn(
                name: "Paymentprogress",
                table: "ContractPaymentDetail",
                newName: "DetailName");

            migrationBuilder.RenameColumn(
                name: "Paymentduedate",
                table: "ContractPaymentDetail",
                newName: "CreatedTime");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "ContractPaymentDetail",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Customervaluepaid",
                table: "ContractPaymentDetail",
                newName: "PaymentRate");

            migrationBuilder.RenameColumn(
                name: "UpdateUsAt",
                table: "Contact",
                newName: "UpdatedTime");

            migrationBuilder.RenameColumn(
                name: "CreatedStAt",
                table: "Contact",
                newName: "CreatedTime");

            migrationBuilder.RenameColumn(
                name: "Dateofsignature",
                table: "Booking",
                newName: "ReservationDate");

            migrationBuilder.AlterColumn<string>(
                name: "Taxcode",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Imagesignature",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "BankNumber",
                table: "Staff",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PeopleApplied",
                table: "Salespolicy",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Block",
                table: "Property",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "Property",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InitialPrice",
                table: "Property",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaintenanceCost",
                table: "Property",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MoneyTax",
                table: "Property",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                table: "Property",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Property",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<double>(
                name: "DepositPrice",
                table: "Project",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "PaymentProcessDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentRate",
                table: "PaymentProcessDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "PaymentProcess",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "PaymentProcess",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Payment",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentTime",
                table: "Payment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReservationTime",
                table: "OpeningForSale",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OpeningForSale",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "OpenForSaleDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "OpenForSaleDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Amountpaid",
                table: "ContractPaymentDetail",
                type: "float",
                nullable: true);

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

            migrationBuilder.AddColumn<double>(
                name: "MoneyReceived",
                table: "ContractPaymentDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MoneyTax",
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

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<byte[]>(
                name: "ContractFile",
                table: "Contact",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractName",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredTime",
                table: "Contact",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Contact",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<byte[]>(
                name: "BookingFile",
                table: "Booking",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "DepositedPrice",
                table: "Booking",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DepositedTimed",
                table: "Booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "Booking",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Block",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "InitialPrice",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "MaintenanceCost",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "MoneyTax",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "DepositPrice",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "PaymentProcessDetail");

            migrationBuilder.DropColumn(
                name: "PaymentRate",
                table: "PaymentProcessDetail");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "PaymentProcess");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "PaymentProcess");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "PaymentTime",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "OpenForSaleDetail");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "OpenForSaleDetail");

            migrationBuilder.DropColumn(
                name: "Amountpaid",
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
                name: "MoneyReceived",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "MoneyTax",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "NumberDayLate",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "ContractFile",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ContractName",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ExpiredTime",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "DepositedPrice",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "DepositedTimed",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "PropertyType",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Property",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "DetailName",
                table: "PaymentProcessDetail",
                newName: "Paymentprocessname");

            migrationBuilder.RenameColumn(
                name: "PaymentProcessName",
                table: "PaymentProcess",
                newName: "Paymentperiod");

            migrationBuilder.RenameColumn(
                name: "PaymentRate",
                table: "ContractPaymentDetail",
                newName: "Customervaluepaid");

            migrationBuilder.RenameColumn(
                name: "DetailName",
                table: "ContractPaymentDetail",
                newName: "Paymentprogress");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ContractPaymentDetail",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "ContractPaymentDetail",
                newName: "Paymentduedate");

            migrationBuilder.RenameColumn(
                name: "UpdatedTime",
                table: "Contact",
                newName: "UpdateUsAt");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "Contact",
                newName: "CreatedStAt");

            migrationBuilder.RenameColumn(
                name: "ReservationDate",
                table: "Booking",
                newName: "Dateofsignature");

            migrationBuilder.AlterColumn<string>(
                name: "Taxcode",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Imagesignature",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BankNumber",
                table: "Staff",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRange",
                table: "Staff",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "PeopleApplied",
                table: "Salespolicy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Project",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<double>(
                name: "Maintenancecosts",
                table: "PaymentProcessDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "PaymentProcess",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PaymentRate",
                table: "PaymentProcess",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerID",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Deposittoholdproject",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReservationTime",
                table: "OpeningForSale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DepositMoney",
                table: "OpeningForSale",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deposittime",
                table: "OpeningForSale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Explain",
                table: "OpeningForSale",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MoneyType",
                table: "OpeningForSale",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "RevervationMoney",
                table: "OpeningForSale",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "OpenForSaleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TypeRoom",
                table: "OpenForSaleDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Contact",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Booking",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "BookingFile",
                table: "Booking",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
