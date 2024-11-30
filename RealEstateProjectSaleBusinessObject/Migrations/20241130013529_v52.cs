using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v52 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "BankNumber",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "DateOfIssue",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "Taxcode",
                table: "Staff");

            migrationBuilder.AlterColumn<string>(
                name: "Placeoforigin",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PlaceOfresidence",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCardNumber",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Placeoforigin",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlaceOfresidence",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCardNumber",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankNumber",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfIssue",
                table: "Staff",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Taxcode",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
