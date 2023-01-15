using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class UpdateTreasuryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treasuries_Currencies_CurrencyId",
                table: "Treasuries");

            migrationBuilder.DropForeignKey(
                name: "FK_Treasuries_DepartmentBankCurrencies_DepartmentBankCurrencyId",
                table: "Treasuries");

            migrationBuilder.DropForeignKey(
                name: "FK_Treasuries_FinancialAccounts_FinancialAccountId",
                table: "Treasuries");

            migrationBuilder.DropIndex(
                name: "IX_Treasuries_FinancialAccountId",
                table: "Treasuries");

            migrationBuilder.DropColumn(
                name: "FinancialAccountId",
                table: "Treasuries");

            migrationBuilder.RenameColumn(
                name: "DepartmentBankCurrencyId",
                table: "Treasuries",
                newName: "BankId");

            migrationBuilder.RenameIndex(
                name: "IX_Treasuries_DepartmentBankCurrencyId",
                table: "Treasuries",
                newName: "IX_Treasuries_BankId");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "Treasuries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNo",
                table: "Treasuries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Treasuries_Banks_BankId",
                table: "Treasuries",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Treasuries_Banks_BankId",
                table: "Treasuries");

            migrationBuilder.DropForeignKey(
                name: "FK_Treasuries_Currencies_CurrencyId",
                table: "Treasuries");

            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Treasuries");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "Treasuries",
                newName: "DepartmentBankCurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Treasuries_BankId",
                table: "Treasuries",
                newName: "IX_Treasuries_DepartmentBankCurrencyId");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "Treasuries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FinancialAccountId",
                table: "Treasuries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Treasuries_FinancialAccountId",
                table: "Treasuries",
                column: "FinancialAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Treasuries_Currencies_CurrencyId",
                table: "Treasuries",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treasuries_DepartmentBankCurrencies_DepartmentBankCurrencyId",
                table: "Treasuries",
                column: "DepartmentBankCurrencyId",
                principalTable: "DepartmentBankCurrencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treasuries_FinancialAccounts_FinancialAccountId",
                table: "Treasuries",
                column: "FinancialAccountId",
                principalTable: "FinancialAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
