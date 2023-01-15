using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class LetterOfGuarantees_NewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "PerformanceLGs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptAttachmentPath",
                table: "PerformanceLGs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptAttachmentTitle",
                table: "PerformanceLGs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivingDate",
                table: "PerformanceLGs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "FinalLGs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptAttachmentPath",
                table: "FinalLGs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptAttachmentTitle",
                table: "FinalLGs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivingDate",
                table: "FinalLGs",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "ReceiptAttachmentPath",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "ReceiptAttachmentTitle",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "ReceivingDate",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "FinalLGs");

            migrationBuilder.DropColumn(
                name: "ReceiptAttachmentPath",
                table: "FinalLGs");

            migrationBuilder.DropColumn(
                name: "ReceiptAttachmentTitle",
                table: "FinalLGs");

            migrationBuilder.DropColumn(
                name: "ReceivingDate",
                table: "FinalLGs");
        }
    }
}
