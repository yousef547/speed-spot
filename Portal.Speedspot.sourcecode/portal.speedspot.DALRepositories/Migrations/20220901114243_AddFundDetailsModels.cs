using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class AddFundDetailsModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FundDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FundPeriod = table.Column<int>(type: "int", nullable: false),
                    BankInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProjectNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractFileId = table.Column<int>(type: "int", nullable: false),
                    CollectionFileId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FundDetails_Attachments_CollectionFileId",
                        column: x => x.CollectionFileId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FundDetails_Attachments_ContractFileId",
                        column: x => x.ContractFileId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FundDetails_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FundDetails_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FundDetails_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundDetailsId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierPayments_FundDetails_FundDetailsId",
                        column: x => x.FundDetailsId,
                        principalTable: "FundDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierPayments_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FundDetails_BankId",
                table: "FundDetails",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_FundDetails_CollectionFileId",
                table: "FundDetails",
                column: "CollectionFileId");

            migrationBuilder.CreateIndex(
                name: "IX_FundDetails_ContractFileId",
                table: "FundDetails",
                column: "ContractFileId");

            migrationBuilder.CreateIndex(
                name: "IX_FundDetails_CurrencyId",
                table: "FundDetails",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_FundDetails_PurchaseOrderId",
                table: "FundDetails",
                column: "PurchaseOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_FundDetailsId",
                table: "SupplierPayments",
                column: "FundDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_SupplierId",
                table: "SupplierPayments",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierPayments");

            migrationBuilder.DropTable(
                name: "FundDetails");
        }
    }
}
