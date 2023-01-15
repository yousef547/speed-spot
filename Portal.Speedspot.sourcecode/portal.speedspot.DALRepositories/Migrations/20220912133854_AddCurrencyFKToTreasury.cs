using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class AddCurrencyFKToTreasury : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Treasuries_CurrencyId",
                table: "Treasuries",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Treasuries_Currencies_CurrencyId",
                table: "Treasuries",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treasuries_Currencies_CurrencyId",
                table: "Treasuries");

            migrationBuilder.DropIndex(
                name: "IX_Treasuries_CurrencyId",
                table: "Treasuries");
        }
    }
}
