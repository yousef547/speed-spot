using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class createsupplierPo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliverPeriod",
                table: "CustomerPos",
                newName: "DeliveryPeriod");

            migrationBuilder.CreateTable(
                name: "SupplierPos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    CustomerPONumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliverPeriod = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierPos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierPos_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierPoOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierOfferId = table.Column<int>(type: "int", nullable: false),
                    SupplierPoId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierPoOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierPoOffers_SupplierOffers_SupplierOfferId",
                        column: x => x.SupplierOfferId,
                        principalTable: "SupplierOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierPoOffers_SupplierPos_SupplierPoId",
                        column: x => x.SupplierPoId,
                        principalTable: "SupplierPos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPoOffers_SupplierOfferId",
                table: "SupplierPoOffers",
                column: "SupplierOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPoOffers_SupplierPoId",
                table: "SupplierPoOffers",
                column: "SupplierPoId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPos_PurchaseOrderId",
                table: "SupplierPos",
                column: "PurchaseOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierPoOffers");

            migrationBuilder.DropTable(
                name: "SupplierPos");

            migrationBuilder.RenameColumn(
                name: "DeliveryPeriod",
                table: "CustomerPos",
                newName: "DeliverPeriod");
        }
    }
}
