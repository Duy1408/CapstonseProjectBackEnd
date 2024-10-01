using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "DateRange",
                table: "Staff",
                newName: "DateOfIssue");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customer",
                newName: "FullName");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCardNumber",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DateOfIssue",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfResidence",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceofOrigin",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfIssue",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PlaceOfResidence",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PlaceofOrigin",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "DateOfIssue",
                table: "Staff",
                newName: "DateRange");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Customer",
                newName: "LastName");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCardNumber",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
