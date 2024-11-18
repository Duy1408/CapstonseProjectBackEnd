using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v48 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeepLink",
                table: "Notification");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Notification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "DeepLink",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
