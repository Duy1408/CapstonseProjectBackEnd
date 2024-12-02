using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v53 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Placeoforigin",
                table: "Staff",
                newName: "PlaceOfOrigin");

            migrationBuilder.RenameColumn(
                name: "PlaceOfresidence",
                table: "Staff",
                newName: "PlaceOfResidence");

            migrationBuilder.RenameColumn(
                name: "PlaceofOrigin",
                table: "Customer",
                newName: "PlaceOfOrigin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlaceOfResidence",
                table: "Staff",
                newName: "PlaceOfresidence");

            migrationBuilder.RenameColumn(
                name: "PlaceOfOrigin",
                table: "Staff",
                newName: "Placeoforigin");

            migrationBuilder.RenameColumn(
                name: "PlaceOfOrigin",
                table: "Customer",
                newName: "PlaceofOrigin");
        }
    }
}
