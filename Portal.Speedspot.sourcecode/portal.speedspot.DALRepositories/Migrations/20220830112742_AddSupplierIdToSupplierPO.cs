using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class AddSupplierIdToSupplierPO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "SupplierPos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPos_SupplierId",
                table: "SupplierPos",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPos_Suppliers_SupplierId",
                table: "SupplierPos",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPos_Suppliers_SupplierId",
                table: "SupplierPos");

            migrationBuilder.DropIndex(
                name: "IX_SupplierPos_SupplierId",
                table: "SupplierPos");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "SupplierPos");
        }
    }
}
