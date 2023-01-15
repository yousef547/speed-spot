using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Quotations_TechnicalSessionAcceptanceColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptanceLetterDate",
                table: "Quotations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcceptanceLetterPath",
                table: "Quotations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinancialSessionDate",
                table: "Quotations",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptanceLetterDate",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "AcceptanceLetterPath",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "FinancialSessionDate",
                table: "Quotations");
        }
    }
}
