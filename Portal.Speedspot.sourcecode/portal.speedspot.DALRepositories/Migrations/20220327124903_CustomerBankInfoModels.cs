using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class CustomerBankInfoModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BeneficiaryAddress",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeneficiaryName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerBanks_BankBranches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "BankBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerBanks_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerBankCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerBankId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBankCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerBankCurrencies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerBankCurrencies_CustomerBanks_CustomerBankId",
                        column: x => x.CustomerBankId,
                        principalTable: "CustomerBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBankCurrencies_CurrencyId",
                table: "CustomerBankCurrencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBankCurrencies_CustomerBankId",
                table: "CustomerBankCurrencies",
                column: "CustomerBankId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBanks_BranchId",
                table: "CustomerBanks",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBanks_CustomerId",
                table: "CustomerBanks",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerBankCurrencies");

            migrationBuilder.DropTable(
                name: "CustomerBanks");

            migrationBuilder.DropColumn(
                name: "BeneficiaryAddress",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BeneficiaryName",
                table: "Customers");
        }
    }
}
