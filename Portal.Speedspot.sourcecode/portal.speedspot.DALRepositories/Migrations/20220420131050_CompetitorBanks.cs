using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class CompetitorBanks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompetitorBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitorId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitorBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitorBanks_BankBranches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "BankBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitorBanks_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalTable: "Competitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitorBankCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitorBankId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitorBankCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitorBankCurrencies_CompetitorBanks_CompetitorBankId",
                        column: x => x.CompetitorBankId,
                        principalTable: "CompetitorBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitorBankCurrencies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorBankCurrencies_CompetitorBankId",
                table: "CompetitorBankCurrencies",
                column: "CompetitorBankId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorBankCurrencies_CurrencyId",
                table: "CompetitorBankCurrencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorBanks_BranchId",
                table: "CompetitorBanks",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorBanks_CompetitorId",
                table: "CompetitorBanks",
                column: "CompetitorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitorBankCurrencies");

            migrationBuilder.DropTable(
                name: "CompetitorBanks");
        }
    }
}
