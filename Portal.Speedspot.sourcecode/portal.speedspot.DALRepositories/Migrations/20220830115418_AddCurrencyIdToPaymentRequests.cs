using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class AddCurrencyIdToPaymentRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "PaymentRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_CurrencyId",
                table: "PaymentRequests",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_Currencies_CurrencyId",
                table: "PaymentRequests",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_Currencies_CurrencyId",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_CurrencyId",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "PaymentRequests");
        }
    }
}
