using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class updattableBankFees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankFees_Banks_bankId",
                table: "BankFees");

            migrationBuilder.RenameColumn(
                name: "bankId",
                table: "BankFees",
                newName: "BankId");

            migrationBuilder.RenameIndex(
                name: "IX_BankFees_bankId",
                table: "BankFees",
                newName: "IX_BankFees_BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankFees_Banks_BankId",
                table: "BankFees",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankFees_Banks_BankId",
                table: "BankFees");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "BankFees",
                newName: "bankId");

            migrationBuilder.RenameIndex(
                name: "IX_BankFees_BankId",
                table: "BankFees",
                newName: "IX_BankFees_bankId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankFees_Banks_bankId",
                table: "BankFees",
                column: "bankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
