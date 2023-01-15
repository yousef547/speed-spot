using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class SupplierBankInfoModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BeneficiaryAddress",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeneficiaryName",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SupplierBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierBanks_BankBranches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "BankBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierBanks_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierBankCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierBankId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierBankCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierBankCurrencies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierBankCurrencies_SupplierBanks_SupplierBankId",
                        column: x => x.SupplierBankId,
                        principalTable: "SupplierBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierBankCurrencies_CurrencyId",
                table: "SupplierBankCurrencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierBankCurrencies_SupplierBankId",
                table: "SupplierBankCurrencies",
                column: "SupplierBankId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierBanks_BranchId",
                table: "SupplierBanks",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierBanks_SupplierId",
                table: "SupplierBanks",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierBankCurrencies");

            migrationBuilder.DropTable(
                name: "SupplierBanks");

            migrationBuilder.DropColumn(
                name: "BeneficiaryAddress",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "BeneficiaryName",
                table: "Suppliers");
        }
    }
}
