using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class createtableproject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedData",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndData",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpportunityId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuotationId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DepartmentId",
                table: "Projects",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OpportunityId",
                table: "Projects",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_QuotationId",
                table: "Projects",
                column: "QuotationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Customers_CustomerId",
                table: "Projects",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Departments_DepartmentId",
                table: "Projects",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Opportunities_OpportunityId",
                table: "Projects",
                column: "OpportunityId",
                principalTable: "Opportunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Quotations_QuotationId",
                table: "Projects",
                column: "QuotationId",
                principalTable: "Quotations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Customers_CustomerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Departments_DepartmentId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Opportunities_OpportunityId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Quotations_QuotationId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DepartmentId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_OpportunityId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_QuotationId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatedData",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EndData",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "OpportunityId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "QuotationId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
