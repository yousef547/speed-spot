using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class AddFinancialAccountIdToTreasury : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinancialAccountId",
                table: "Treasuries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Treasuries_FinancialAccountId",
                table: "Treasuries",
                column: "FinancialAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Treasuries_FinancialAccounts_FinancialAccountId",
                table: "Treasuries",
                column: "FinancialAccountId",
                principalTable: "FinancialAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treasuries_FinancialAccounts_FinancialAccountId",
                table: "Treasuries");

            migrationBuilder.DropIndex(
                name: "IX_Treasuries_FinancialAccountId",
                table: "Treasuries");

            migrationBuilder.DropColumn(
                name: "FinancialAccountId",
                table: "Treasuries");
        }
    }
}
