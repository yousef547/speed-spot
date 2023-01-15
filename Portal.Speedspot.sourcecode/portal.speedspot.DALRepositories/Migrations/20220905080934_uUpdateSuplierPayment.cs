using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class uUpdateSuplierPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "SupplierPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_PaymentTypeId",
                table: "SupplierPayments",
                column: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_PaymentTypes_PaymentTypeId",
                table: "SupplierPayments",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_PaymentTypes_PaymentTypeId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_SupplierPayments_PaymentTypeId",
                table: "SupplierPayments");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "SupplierPayments");
        }
    }
}
