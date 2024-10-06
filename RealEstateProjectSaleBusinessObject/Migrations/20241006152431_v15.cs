using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_DocumentTemplate_DocumentTemplateDocumentID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_DocumentTemplate_DocumentTemplateDocumentID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Booking_BookingID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_BookingID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "BookingID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "DocumentID",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "DocumentID",
                table: "DocumentTemplate",
                newName: "DocumentTemplateID");

            migrationBuilder.RenameColumn(
                name: "DocumentTemplateDocumentID",
                table: "Contact",
                newName: "DocumentTemplateID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_DocumentTemplateDocumentID",
                table: "Contact",
                newName: "IX_Contact_DocumentTemplateID");

            migrationBuilder.RenameColumn(
                name: "DocumentTemplateDocumentID",
                table: "Booking",
                newName: "PropertyID");

            migrationBuilder.RenameColumn(
                name: "DocumentID",
                table: "Booking",
                newName: "DocumentTemplateID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DocumentTemplateDocumentID",
                table: "Booking",
                newName: "IX_Booking_PropertyID");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "UnitType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProjectCategoryDetail",
                columns: table => new
                {
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCategoryDetail", x => new { x.ProjectID, x.PropertyCategoryID });
                    table.ForeignKey(
                        name: "FK_ProjectCategoryDetail_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectCategoryDetail_PropertyCategory_PropertyCategoryID",
                        column: x => x.PropertyCategoryID,
                        principalTable: "PropertyCategory",
                        principalColumn: "PropertyCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitType_ProjectID",
                table: "UnitType",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_DocumentTemplateID",
                table: "Booking",
                column: "DocumentTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ProjectID",
                table: "Booking",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategoryDetail_PropertyCategoryID",
                table: "ProjectCategoryDetail",
                column: "PropertyCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_DocumentTemplate_DocumentTemplateID",
                table: "Booking",
                column: "DocumentTemplateID",
                principalTable: "DocumentTemplate",
                principalColumn: "DocumentTemplateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Project_ProjectID",
                table: "Booking",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Property_PropertyID",
                table: "Booking",
                column: "PropertyID",
                principalTable: "Property",
                principalColumn: "PropertyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_DocumentTemplate_DocumentTemplateID",
                table: "Contact",
                column: "DocumentTemplateID",
                principalTable: "DocumentTemplate",
                principalColumn: "DocumentTemplateID");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitType_Project_ProjectID",
                table: "UnitType",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_DocumentTemplate_DocumentTemplateID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Project_ProjectID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Property_PropertyID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_DocumentTemplate_DocumentTemplateID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitType_Project_ProjectID",
                table: "UnitType");

            migrationBuilder.DropTable(
                name: "ProjectCategoryDetail");

            migrationBuilder.DropIndex(
                name: "IX_UnitType_ProjectID",
                table: "UnitType");

            migrationBuilder.DropIndex(
                name: "IX_Booking_DocumentTemplateID",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ProjectID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "UnitType");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "DocumentTemplateID",
                table: "DocumentTemplate",
                newName: "DocumentID");

            migrationBuilder.RenameColumn(
                name: "DocumentTemplateID",
                table: "Contact",
                newName: "DocumentTemplateDocumentID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_DocumentTemplateID",
                table: "Contact",
                newName: "IX_Contact_DocumentTemplateDocumentID");

            migrationBuilder.RenameColumn(
                name: "PropertyID",
                table: "Booking",
                newName: "DocumentTemplateDocumentID");

            migrationBuilder.RenameColumn(
                name: "DocumentTemplateID",
                table: "Booking",
                newName: "DocumentID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_PropertyID",
                table: "Booking",
                newName: "IX_Booking_DocumentTemplateDocumentID");

            migrationBuilder.AddColumn<Guid>(
                name: "BookingID",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentID",
                table: "Contact",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Property_BookingID",
                table: "Property",
                column: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_DocumentTemplate_DocumentTemplateDocumentID",
                table: "Booking",
                column: "DocumentTemplateDocumentID",
                principalTable: "DocumentTemplate",
                principalColumn: "DocumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_DocumentTemplate_DocumentTemplateDocumentID",
                table: "Contact",
                column: "DocumentTemplateDocumentID",
                principalTable: "DocumentTemplate",
                principalColumn: "DocumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Booking_BookingID",
                table: "Property",
                column: "BookingID",
                principalTable: "Booking",
                principalColumn: "BookingID");
        }
    }
}
