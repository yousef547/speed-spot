using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class updatepurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerPos_PurchaseOrderId",
                table: "CustomerPos");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPos_PurchaseOrderId",
                table: "CustomerPos",
                column: "PurchaseOrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerPos_PurchaseOrderId",
                table: "CustomerPos");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPos_PurchaseOrderId",
                table: "CustomerPos",
                column: "PurchaseOrderId");
        }
    }
}
