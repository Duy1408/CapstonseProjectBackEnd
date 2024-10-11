using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "UnitType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PropertyType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PropertyCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PaymentType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PaymentProcess",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Notification",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "DocumentTemplate",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UnitType");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PropertyType");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PropertyCategory");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PaymentType");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PaymentProcess");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DocumentTemplate");
        }
    }
}
