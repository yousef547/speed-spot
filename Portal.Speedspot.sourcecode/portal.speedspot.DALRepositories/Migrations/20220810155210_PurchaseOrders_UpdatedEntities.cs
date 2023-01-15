using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class PurchaseOrders_UpdatedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedVersionId",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PurchaseOrderSupplierOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    SupplierOfferId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderSupplierOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderSupplierOffers_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderSupplierOffers_SupplierOffers_SupplierOfferId",
                        column: x => x.SupplierOfferId,
                        principalTable: "SupplierOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SelectedVersionId",
                table: "PurchaseOrders",
                column: "SelectedVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderSupplierOffers_PurchaseOrderId",
                table: "PurchaseOrderSupplierOffers",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderSupplierOffers_SupplierOfferId",
                table: "PurchaseOrderSupplierOffers",
                column: "SupplierOfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_QuotationOfferVersions_SelectedVersionId",
                table: "PurchaseOrders",
                column: "SelectedVersionId",
                principalTable: "QuotationOfferVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_QuotationOfferVersions_SelectedVersionId",
                table: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "PurchaseOrderSupplierOffers");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_SelectedVersionId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "SelectedVersionId",
                table: "PurchaseOrders");
        }
    }
}
