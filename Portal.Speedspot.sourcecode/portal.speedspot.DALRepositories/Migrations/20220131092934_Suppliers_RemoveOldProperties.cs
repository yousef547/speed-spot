using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Suppliers_RemoveOldProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Banks_BankId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Countries_CountryId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_BankId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_CountryId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Suppliers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_BankId",
                table: "Suppliers",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CountryId",
                table: "Suppliers",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Banks_BankId",
                table: "Suppliers",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Countries_CountryId",
                table: "Suppliers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
