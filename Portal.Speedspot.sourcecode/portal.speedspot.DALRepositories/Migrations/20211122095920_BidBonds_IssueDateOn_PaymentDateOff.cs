using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class BidBonds_IssueDateOn_PaymentDateOff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IssueDate",
                table: "BidBonds",
                newName: "BidBondOn_IssueDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "BidBondOff_PaymentDate",
                table: "BidBonds",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BidBondOff_PaymentDate",
                table: "BidBonds");

            migrationBuilder.RenameColumn(
                name: "BidBondOn_IssueDate",
                table: "BidBonds",
                newName: "IssueDate");
        }
    }
}
