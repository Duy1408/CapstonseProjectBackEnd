using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractFile",
                table: "Contract",
                newName: "ContractSaleFile");

            migrationBuilder.AddColumn<string>(
                name: "ContractDepositFile",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractDepositFile",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "ContractSaleFile",
                table: "Contract",
                newName: "ContractFile");
        }
    }
}
