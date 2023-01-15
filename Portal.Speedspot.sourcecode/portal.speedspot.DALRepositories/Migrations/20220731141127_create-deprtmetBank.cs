using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class createdeprtmetBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentBanks_BankBranches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "BankBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentBanks_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentBankCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentBankId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentBankCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentBankCurrencies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentBankCurrencies_DepartmentBanks_DepartmentBankId",
                        column: x => x.DepartmentBankId,
                        principalTable: "DepartmentBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBankCurrencies_CurrencyId",
                table: "DepartmentBankCurrencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBankCurrencies_DepartmentBankId",
                table: "DepartmentBankCurrencies",
                column: "DepartmentBankId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBanks_BranchId",
                table: "DepartmentBanks",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBanks_DepartmentId",
                table: "DepartmentBanks",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentBankCurrencies");

            migrationBuilder.DropTable(
                name: "DepartmentBanks");
        }
    }
}
