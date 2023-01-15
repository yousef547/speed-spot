using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Quotations_AwardingLetterColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AwardingLetterDate",
                table: "Quotations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AwardingLetterPath",
                table: "Quotations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwardingLetterDate",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "AwardingLetterPath",
                table: "Quotations");
        }
    }
}
