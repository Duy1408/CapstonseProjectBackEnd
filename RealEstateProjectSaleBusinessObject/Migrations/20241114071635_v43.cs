using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateProjectSaleBusinessObject.Migrations
{
    public partial class v43 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPolicy_Project_ProjectID",
                table: "PaymentPolicy");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPolicy_ProjectID",
                table: "PaymentPolicy");

            migrationBuilder.DropColumn(
                name: "EarlyDate",
                table: "PaymentPolicy");

            migrationBuilder.DropColumn(
                name: "PercentEarly",
                table: "PaymentPolicy");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "PaymentPolicy");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentPolicyID",
                table: "Project",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_PaymentPolicyID",
                table: "Project",
                column: "PaymentPolicyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_PaymentPolicy_PaymentPolicyID",
                table: "Project",
                column: "PaymentPolicyID",
                principalTable: "PaymentPolicy",
                principalColumn: "PaymentPolicyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_PaymentPolicy_PaymentPolicyID",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_PaymentPolicyID",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "PaymentPolicyID",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "EarlyDate",
                table: "PaymentPolicy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PercentEarly",
                table: "PaymentPolicy",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "PaymentPolicy",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPolicy_ProjectID",
                table: "PaymentPolicy",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPolicy_Project_ProjectID",
                table: "PaymentPolicy",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID");
        }
    }
}
