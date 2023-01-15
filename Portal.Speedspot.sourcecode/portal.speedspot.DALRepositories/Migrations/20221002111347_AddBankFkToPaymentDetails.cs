using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class AddBankFkToPaymentDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_ReceiveBankId",
                table: "PaymentDetails",
                column: "ReceiveBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_Banks_ReceiveBankId",
                table: "PaymentDetails",
                column: "ReceiveBankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_Banks_ReceiveBankId",
                table: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDetails_ReceiveBankId",
                table: "PaymentDetails");
        }
    }
}
