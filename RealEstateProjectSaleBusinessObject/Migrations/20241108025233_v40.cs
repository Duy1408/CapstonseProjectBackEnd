using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "PromotionDetail");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "PromotionDetail");

            migrationBuilder.DropColumn(
                name: "PromotionType",
                table: "PromotionDetail");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "DetailName",
                table: "PaymentProcessDetail");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "PaymentProcessDetail");

            migrationBuilder.DropColumn(
                name: "PaymentRate",
                table: "PaymentProcessDetail");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "PaymentProcessDetail");

            migrationBuilder.DropColumn(
                name: "PeriodType",
                table: "PaymentProcessDetail");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "PaymentProcess");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "PaymentProcess");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Period",
                table: "PaymentProcessDetail",
                type: "date",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "PaymentProcessDetail",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStage",
                table: "PaymentProcessDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Percentage",
                table: "PaymentProcessDetail",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStage",
                table: "PaymentProcessDetail");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "PaymentProcessDetail");

            migrationBuilder.AddColumn<double>(
                name: "DiscountAmount",
                table: "PromotionDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercent",
                table: "PromotionDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionType",
                table: "PromotionDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Promotion",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Promotion",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Period",
                table: "PaymentProcessDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "PaymentProcessDetail",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailName",
                table: "PaymentProcessDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "PaymentProcessDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PaymentRate",
                table: "PaymentProcessDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "PaymentProcessDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PeriodType",
                table: "PaymentProcessDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "PaymentProcess",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "PaymentProcess",
                type: "float",
                nullable: true);
        }
    }
}
