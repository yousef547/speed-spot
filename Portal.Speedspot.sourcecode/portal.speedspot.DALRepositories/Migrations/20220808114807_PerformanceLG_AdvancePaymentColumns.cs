using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class PerformanceLG_AdvancePaymentColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AdvanceAmount",
                table: "PerformanceLGs",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdvanceAttachmentPath",
                table: "PerformanceLGs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdvanceAttachmentTitle",
                table: "PerformanceLGs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AdvanceDate",
                table: "PerformanceLGs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdvanceNo",
                table: "PerformanceLGs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdvanceType",
                table: "PerformanceLGs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IssueBankId",
                table: "PerformanceLGs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiveBankId",
                table: "PerformanceLGs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceLGs_IssueBankId",
                table: "PerformanceLGs",
                column: "IssueBankId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceLGs_ReceiveBankId",
                table: "PerformanceLGs",
                column: "ReceiveBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGs_Banks_IssueBankId",
                table: "PerformanceLGs",
                column: "IssueBankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGs_Banks_ReceiveBankId",
                table: "PerformanceLGs",
                column: "ReceiveBankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGs_Banks_IssueBankId",
                table: "PerformanceLGs");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGs_Banks_ReceiveBankId",
                table: "PerformanceLGs");

            migrationBuilder.DropIndex(
                name: "IX_PerformanceLGs_IssueBankId",
                table: "PerformanceLGs");

            migrationBuilder.DropIndex(
                name: "IX_PerformanceLGs_ReceiveBankId",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "AdvanceAmount",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "AdvanceAttachmentPath",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "AdvanceAttachmentTitle",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "AdvanceDate",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "AdvanceNo",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "AdvanceType",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "IssueBankId",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "ReceiveBankId",
                table: "PerformanceLGs");
        }
    }
}
