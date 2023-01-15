using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class UpdatePaymentRequestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "PaymentRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "PaymentRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SerialNo",
                table: "PaymentRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "SerialNo",
                table: "PaymentRequests");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "PaymentRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
