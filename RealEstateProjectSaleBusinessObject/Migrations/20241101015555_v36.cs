using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCategoryDetail_Project_ProjectID",
                table: "ProjectCategoryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCategoryDetail_PropertyCategory_PropertyCategoryID",
                table: "ProjectCategoryDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCategoryDetail_Project_ProjectID",
                table: "ProjectCategoryDetail",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCategoryDetail_PropertyCategory_PropertyCategoryID",
                table: "ProjectCategoryDetail",
                column: "PropertyCategoryID",
                principalTable: "PropertyCategory",
                principalColumn: "PropertyCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCategoryDetail_Project_ProjectID",
                table: "ProjectCategoryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCategoryDetail_PropertyCategory_PropertyCategoryID",
                table: "ProjectCategoryDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCategoryDetail_Project_ProjectID",
                table: "ProjectCategoryDetail",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCategoryDetail_PropertyCategory_PropertyCategoryID",
                table: "ProjectCategoryDetail",
                column: "PropertyCategoryID",
                principalTable: "PropertyCategory",
                principalColumn: "PropertyCategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
