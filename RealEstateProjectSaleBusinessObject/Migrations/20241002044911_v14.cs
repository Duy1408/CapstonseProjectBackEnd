using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zone_PropertyCategory_PropertyCategoryID",
                table: "Zone");

            migrationBuilder.RenameColumn(
                name: "PropertyCategoryID",
                table: "Zone",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Zone_PropertyCategoryID",
                table: "Zone",
                newName: "IX_Zone_ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_Project_ProjectID",
                table: "Zone",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zone_Project_ProjectID",
                table: "Zone");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "Zone",
                newName: "PropertyCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Zone_ProjectID",
                table: "Zone",
                newName: "IX_Zone_PropertyCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_PropertyCategory_PropertyCategoryID",
                table: "Zone",
                column: "PropertyCategoryID",
                principalTable: "PropertyCategory",
                principalColumn: "PropertyCategoryID");
        }
    }
}
