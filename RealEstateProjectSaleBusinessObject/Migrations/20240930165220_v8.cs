using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Project_ProjectID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Property_PropertyID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Booking_BookingID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_ContractPaymentDetail_ContractPaymentDetailID",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionDetail_PropertyType_PropertiesTypeID",
                table: "PromotionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Project_ProjectID",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_PropertyType_PropertyTypeID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_ProjectID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_PropertyTypeID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_PromotionDetail_PropertiesTypeID",
                table: "PromotionDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenForSaleDetail",
                table: "OpenForSaleDetail");

            migrationBuilder.DropIndex(
                name: "IX_OpenForSaleDetail_OpeningForSaleID",
                table: "OpenForSaleDetail");

            migrationBuilder.DropColumn(
                name: "BathRoom",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "BedRoom",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Block",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "InitialPrice",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "LivingRoom",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "MaintenanceCost",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "PropertyTypeID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "SizeArea",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CommericalName",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Commune",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DateOfIssue",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DepositPrice",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "LicenseNo",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ReservationTime",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "OpenForSaleDetailID",
                table: "OpenForSaleDetail");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "OpenForSaleDetail");

            migrationBuilder.DropColumn(
                name: "PersonalEmail",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ReservationDate",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "PropertyType",
                newName: "PropertyTypeName");

            migrationBuilder.RenameColumn(
                name: "PropertyName",
                table: "Property",
                newName: "PropertyCode");

            migrationBuilder.RenameColumn(
                name: "MoneyTax",
                table: "Property",
                newName: "PriceSold");

            migrationBuilder.RenameColumn(
                name: "TypeOfProject",
                table: "Project",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Project",
                newName: "TotalNumberOfApartment");

            migrationBuilder.RenameColumn(
                name: "PlaceofIssue",
                table: "Project",
                newName: "TotalArea");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Project",
                newName: "Scale");

            migrationBuilder.RenameColumn(
                name: "CampusArea",
                table: "Project",
                newName: "ProductType");

            migrationBuilder.RenameColumn(
                name: "ContractPaymentDetailID",
                table: "Payment",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_ContractPaymentDetailID",
                table: "Payment",
                newName: "IX_Payment_CustomerID");

            migrationBuilder.RenameColumn(
                name: "DescriptionName",
                table: "OpeningForSale",
                newName: "ReservationPrice");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                table: "OpeningForSale",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "DateEnd",
                table: "OpeningForSale",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "PropertyID",
                table: "Booking",
                newName: "PropertyCategoryID");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "Booking",
                newName: "DocumentTemplateDocumentID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_PropertyID",
                table: "Booking",
                newName: "IX_Booking_PropertyCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_ProjectID",
                table: "Booking",
                newName: "IX_Booking_DocumentTemplateDocumentID");

            migrationBuilder.AddColumn<string>(
                name: "DateRange",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyCategoryID",
                table: "PropertyType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlockID",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingID",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FloorID",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitTypeID",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ZoneID",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "PromotionDetail",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyTypeID",
                table: "PromotionDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingDensity",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Convenience",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesignUnit",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeneralContractor",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HandOver",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Investor",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalStatus",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PaymentRate",
                table: "PaymentProcessDetail",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "PaymentProcess",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookingID",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckinDate",
                table: "OpeningForSale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DecisionName",
                table: "OpeningForSale",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RemittanceOrder",
                table: "ContractPaymentDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentProcessID",
                table: "Contact",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ContractFile",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractCode",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentID",
                table: "Contact",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentTemplateDocumentID",
                table: "Contact",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StaffID",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "BookingFile",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentID",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OpenForSaleID",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenForSaleDetail",
                table: "OpenForSaleDetail",
                columns: new[] { "OpeningForSaleID", "PropertyID" });

            migrationBuilder.CreateTable(
                name: "DocumentTemplate",
                columns: table => new
                {
                    DocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentFile = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTemplate", x => x.DocumentID);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtiltle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeepLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OpenForSaleID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OpeningForSaleID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notification_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK_Notification_OpeningForSale_OpeningForSaleID",
                        column: x => x.OpeningForSaleID,
                        principalTable: "OpeningForSale",
                        principalColumn: "OpeningForSaleID");
                });

            migrationBuilder.CreateTable(
                name: "PaymentPolicy",
                columns: table => new
                {
                    PaymentPolicyID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentPolicyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PercentEarly = table.Column<double>(type: "float", nullable: true),
                    EarlyDate = table.Column<int>(type: "int", nullable: true),
                    LateDate = table.Column<int>(type: "int", nullable: true),
                    PercentLate = table.Column<double>(type: "float", nullable: true),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPolicy", x => x.PaymentPolicyID);
                    table.ForeignKey(
                        name: "FK_PaymentPolicy_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateTable(
                name: "PropertyCategory",
                columns: table => new
                {
                    PropertyCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyCategory", x => x.PropertyCategoryID);
                    table.ForeignKey(
                        name: "FK_PropertyCategory_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateTable(
                name: "UnitType",
                columns: table => new
                {
                    UnitTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BathRoom = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetFloorArea = table.Column<double>(type: "float", nullable: true),
                    GrossFloorArea = table.Column<double>(type: "float", nullable: true),
                    BedRoom = table.Column<int>(type: "int", nullable: false),
                    KitchenRoom = table.Column<int>(type: "int", nullable: false),
                    LivingRoom = table.Column<int>(type: "int", nullable: false),
                    NumberFloor = table.Column<int>(type: "int", nullable: true),
                    Basement = table.Column<int>(type: "int", nullable: true),
                    PropertyTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitType", x => x.UnitTypeID);
                    table.ForeignKey(
                        name: "FK_UnitType_PropertyType_PropertyTypeID",
                        column: x => x.PropertyTypeID,
                        principalTable: "PropertyType",
                        principalColumn: "PropertyTypeID");
                });

            migrationBuilder.CreateTable(
                name: "Zone",
                columns: table => new
                {
                    ZoneID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyCategoryy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.ZoneID);
                    table.ForeignKey(
                        name: "FK_Zone_PropertyCategory_PropertyCategoryID",
                        column: x => x.PropertyCategoryID,
                        principalTable: "PropertyCategory",
                        principalColumn: "PropertyCategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    BlockID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlockName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageBlock = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZoneID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.BlockID);
                    table.ForeignKey(
                        name: "FK_Block_Zone_ZoneID",
                        column: x => x.ZoneID,
                        principalTable: "Zone",
                        principalColumn: "ZoneID");
                });

            migrationBuilder.CreateTable(
                name: "Floor",
                columns: table => new
                {
                    FloorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumFloor = table.Column<int>(type: "int", nullable: false),
                    ImageFloor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlockID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floor", x => x.FloorID);
                    table.ForeignKey(
                        name: "FK_Floor_Block_BlockID",
                        column: x => x.BlockID,
                        principalTable: "Block",
                        principalColumn: "BlockID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyType_PropertyCategoryID",
                table: "PropertyType",
                column: "PropertyCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_BlockID",
                table: "Property",
                column: "BlockID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_BookingID",
                table: "Property",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_FloorID",
                table: "Property",
                column: "FloorID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_UnitTypeID",
                table: "Property",
                column: "UnitTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ZoneID",
                table: "Property",
                column: "ZoneID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDetail_PropertyTypeID",
                table: "PromotionDetail",
                column: "PropertyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_DocumentTemplateDocumentID",
                table: "Contact",
                column: "DocumentTemplateDocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Block_ZoneID",
                table: "Block",
                column: "ZoneID");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_BlockID",
                table: "Floor",
                column: "BlockID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CustomerID",
                table: "Notification",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_OpeningForSaleID",
                table: "Notification",
                column: "OpeningForSaleID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPolicy_ProjectID",
                table: "PaymentPolicy",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyCategory_ProjectID",
                table: "PropertyCategory",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitType_PropertyTypeID",
                table: "UnitType",
                column: "PropertyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_PropertyCategoryID",
                table: "Zone",
                column: "PropertyCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_DocumentTemplate_DocumentTemplateDocumentID",
                table: "Booking",
                column: "DocumentTemplateDocumentID",
                principalTable: "DocumentTemplate",
                principalColumn: "DocumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_PropertyCategory_PropertyCategoryID",
                table: "Booking",
                column: "PropertyCategoryID",
                principalTable: "PropertyCategory",
                principalColumn: "PropertyCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Booking_BookingID",
                table: "Contact",
                column: "BookingID",
                principalTable: "Booking",
                principalColumn: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_DocumentTemplate_DocumentTemplateDocumentID",
                table: "Contact",
                column: "DocumentTemplateDocumentID",
                principalTable: "DocumentTemplate",
                principalColumn: "DocumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Customer_CustomerID",
                table: "Payment",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionDetail_PropertyType_PropertyTypeID",
                table: "PromotionDetail",
                column: "PropertyTypeID",
                principalTable: "PropertyType",
                principalColumn: "PropertyTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Block_BlockID",
                table: "Property",
                column: "BlockID",
                principalTable: "Block",
                principalColumn: "BlockID");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Booking_BookingID",
                table: "Property",
                column: "BookingID",
                principalTable: "Booking",
                principalColumn: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Floor_FloorID",
                table: "Property",
                column: "FloorID",
                principalTable: "Floor",
                principalColumn: "FloorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_UnitType_UnitTypeID",
                table: "Property",
                column: "UnitTypeID",
                principalTable: "UnitType",
                principalColumn: "UnitTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Zone_ZoneID",
                table: "Property",
                column: "ZoneID",
                principalTable: "Zone",
                principalColumn: "ZoneID");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyType_PropertyCategory_PropertyCategoryID",
                table: "PropertyType",
                column: "PropertyCategoryID",
                principalTable: "PropertyCategory",
                principalColumn: "PropertyCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_DocumentTemplate_DocumentTemplateDocumentID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_PropertyCategory_PropertyCategoryID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Booking_BookingID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_DocumentTemplate_DocumentTemplateDocumentID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Customer_CustomerID",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionDetail_PropertyType_PropertyTypeID",
                table: "PromotionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Block_BlockID",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Booking_BookingID",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Floor_FloorID",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_UnitType_UnitTypeID",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Zone_ZoneID",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyType_PropertyCategory_PropertyCategoryID",
                table: "PropertyType");

            migrationBuilder.DropTable(
                name: "DocumentTemplate");

            migrationBuilder.DropTable(
                name: "Floor");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PaymentPolicy");

            migrationBuilder.DropTable(
                name: "UnitType");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "Zone");

            migrationBuilder.DropTable(
                name: "PropertyCategory");

            migrationBuilder.DropIndex(
                name: "IX_PropertyType_PropertyCategoryID",
                table: "PropertyType");

            migrationBuilder.DropIndex(
                name: "IX_Property_BlockID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_BookingID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_FloorID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_UnitTypeID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_ZoneID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_PromotionDetail_PropertyTypeID",
                table: "PromotionDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenForSaleDetail",
                table: "OpenForSaleDetail");

            migrationBuilder.DropIndex(
                name: "IX_Contact_DocumentTemplateDocumentID",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "DateRange",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "PropertyCategoryID",
                table: "PropertyType");

            migrationBuilder.DropColumn(
                name: "BlockID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "BookingID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "FloorID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "UnitTypeID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "ZoneID",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "PropertyTypeID",
                table: "PromotionDetail");

            migrationBuilder.DropColumn(
                name: "BuildingDensity",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Convenience",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DesignUnit",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "GeneralContractor",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "HandOver",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Investor",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "LegalStatus",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CheckinDate",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "DecisionName",
                table: "OpeningForSale");

            migrationBuilder.DropColumn(
                name: "RemittanceOrder",
                table: "ContractPaymentDetail");

            migrationBuilder.DropColumn(
                name: "ContractCode",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "DocumentID",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "DocumentTemplateDocumentID",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "DocumentID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "OpenForSaleID",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "PropertyTypeName",
                table: "PropertyType",
                newName: "TypeName");

            migrationBuilder.RenameColumn(
                name: "PropertyCode",
                table: "Property",
                newName: "PropertyName");

            migrationBuilder.RenameColumn(
                name: "PriceSold",
                table: "Property",
                newName: "MoneyTax");

            migrationBuilder.RenameColumn(
                name: "TotalNumberOfApartment",
                table: "Project",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "TotalArea",
                table: "Project",
                newName: "PlaceofIssue");

            migrationBuilder.RenameColumn(
                name: "Scale",
                table: "Project",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "ProductType",
                table: "Project",
                newName: "CampusArea");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Project",
                newName: "TypeOfProject");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Payment",
                newName: "ContractPaymentDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_CustomerID",
                table: "Payment",
                newName: "IX_Payment_ContractPaymentDetailID");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "OpeningForSale",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "ReservationPrice",
                table: "OpeningForSale",
                newName: "DescriptionName");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "OpeningForSale",
                newName: "DateEnd");

            migrationBuilder.RenameColumn(
                name: "PropertyCategoryID",
                table: "Booking",
                newName: "PropertyID");

            migrationBuilder.RenameColumn(
                name: "DocumentTemplateDocumentID",
                table: "Booking",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_PropertyCategoryID",
                table: "Booking",
                newName: "IX_Booking_PropertyID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DocumentTemplateDocumentID",
                table: "Booking",
                newName: "IX_Booking_ProjectID");

            migrationBuilder.AddColumn<int>(
                name: "BathRoom",
                table: "Property",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BedRoom",
                table: "Property",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Property",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InitialPrice",
                table: "Property",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "LivingRoom",
                table: "Property",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MaintenanceCost",
                table: "Property",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "Property",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyTypeID",
                table: "Property",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "SizeArea",
                table: "Property",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Property",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "PromotionDetail",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CommericalName",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Commune",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfIssue",
                table: "Project",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DepositPrice",
                table: "Project",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LicenseNo",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentRate",
                table: "PaymentProcessDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "PaymentProcess",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BookingID",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservationTime",
                table: "OpeningForSale",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OpenForSaleDetailID",
                table: "OpenForSaleDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "OpenForSaleDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalEmail",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentProcessID",
                table: "Contact",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "ContractFile",
                table: "Contact",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StaffID",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "BookingFile",
                table: "Booking",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationDate",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenForSaleDetail",
                table: "OpenForSaleDetail",
                column: "OpenForSaleDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ProjectID",
                table: "Property",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_PropertyTypeID",
                table: "Property",
                column: "PropertyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDetail_PropertiesTypeID",
                table: "PromotionDetail",
                column: "PropertiesTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_OpenForSaleDetail_OpeningForSaleID",
                table: "OpenForSaleDetail",
                column: "OpeningForSaleID");

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
                name: "FK_Contact_Booking_BookingID",
                table: "Contact",
                column: "BookingID",
                principalTable: "Booking",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_ContractPaymentDetail_ContractPaymentDetailID",
                table: "Payment",
                column: "ContractPaymentDetailID",
                principalTable: "ContractPaymentDetail",
                principalColumn: "ContractPaymentDetailID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionDetail_PropertyType_PropertiesTypeID",
                table: "PromotionDetail",
                column: "PropertiesTypeID",
                principalTable: "PropertyType",
                principalColumn: "PropertyTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Project_ProjectID",
                table: "Property",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_PropertyType_PropertyTypeID",
                table: "Property",
                column: "PropertyTypeID",
                principalTable: "PropertyType",
                principalColumn: "PropertyTypeID");
        }
    }
}
