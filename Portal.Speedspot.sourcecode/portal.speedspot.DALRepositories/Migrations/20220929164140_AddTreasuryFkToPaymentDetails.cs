using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class AddTreasuryFkToPaymentDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_TreasuryId",
                table: "PaymentDetails",
                column: "TreasuryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_Treasuries_TreasuryId",
                table: "PaymentDetails",
                column: "TreasuryId",
                principalTable: "Treasuries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_Treasuries_TreasuryId",
                table: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDetails_TreasuryId",
                table: "PaymentDetails");
        }
    }
}
