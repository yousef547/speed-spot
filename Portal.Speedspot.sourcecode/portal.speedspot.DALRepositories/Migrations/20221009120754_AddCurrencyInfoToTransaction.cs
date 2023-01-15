using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class AddCurrencyInfoToTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "TreasuryTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRate",
                table: "TreasuryTransactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "FinancialAccountTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRate",
                table: "FinancialAccountTransactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_TreasuryTransactions_CurrencyId",
                table: "TreasuryTransactions",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAccountTransactions_CurrencyId",
                table: "FinancialAccountTransactions",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAccountTransactions_Currencies_CurrencyId",
                table: "FinancialAccountTransactions",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TreasuryTransactions_Currencies_CurrencyId",
                table: "TreasuryTransactions",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAccountTransactions_Currencies_CurrencyId",
                table: "FinancialAccountTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_TreasuryTransactions_Currencies_CurrencyId",
                table: "TreasuryTransactions");

            migrationBuilder.DropIndex(
                name: "IX_TreasuryTransactions_CurrencyId",
                table: "TreasuryTransactions");

            migrationBuilder.DropIndex(
                name: "IX_FinancialAccountTransactions_CurrencyId",
                table: "FinancialAccountTransactions");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "TreasuryTransactions");

            migrationBuilder.DropColumn(
                name: "ExchangeRate",
                table: "TreasuryTransactions");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "FinancialAccountTransactions");

            migrationBuilder.DropColumn(
                name: "ExchangeRate",
                table: "FinancialAccountTransactions");
        }
    }
}
