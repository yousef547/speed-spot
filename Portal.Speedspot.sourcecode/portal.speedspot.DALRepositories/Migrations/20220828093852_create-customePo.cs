using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class createcustomePo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerPos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    CustomerPONumber = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliverPeriod = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPos_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPoAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerPoId = table.Column<int>(type: "int", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPoAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPoAttachments_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict );
                    table.ForeignKey(
                        name: "FK_CustomerPoAttachments_CustomerPos_CustomerPoId",
                        column: x => x.CustomerPoId,
                        principalTable: "CustomerPos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPoAttachments_AttachmentId",
                table: "CustomerPoAttachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPoAttachments_CustomerPoId",
                table: "CustomerPoAttachments",
                column: "CustomerPoId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPos_PurchaseOrderId",
                table: "CustomerPos",
                column: "PurchaseOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerPoAttachments");

            migrationBuilder.DropTable(
                name: "CustomerPos");
        }
    }
}
