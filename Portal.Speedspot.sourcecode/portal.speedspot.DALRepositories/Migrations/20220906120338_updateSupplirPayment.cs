using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class updateSupplirPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_Suppliers_SupplierId",
                table: "SupplierPayments");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "SupplierPayments",
                newName: "SupplierPoId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierPayments_SupplierId",
                table: "SupplierPayments",
                newName: "IX_SupplierPayments_SupplierPoId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_SupplierPos_SupplierPoId",
                table: "SupplierPayments",
                column: "SupplierPoId",
                principalTable: "SupplierPos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_SupplierPos_SupplierPoId",
                table: "SupplierPayments");

            migrationBuilder.RenameColumn(
                name: "SupplierPoId",
                table: "SupplierPayments",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierPayments_SupplierPoId",
                table: "SupplierPayments",
                newName: "IX_SupplierPayments_SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_Suppliers_SupplierId",
                table: "SupplierPayments",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
