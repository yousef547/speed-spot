using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class BidBonds_Updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidBonds_Banks_BankId",
                table: "BidBonds");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "BidBonds",
                newName: "BidBondOn_BankId");

            migrationBuilder.RenameIndex(
                name: "IX_BidBonds_BankId",
                table: "BidBonds",
                newName: "IX_BidBonds_BidBondOn_BankId");

            migrationBuilder.AddColumn<bool>(
                name: "BidBondOn_IsCredit",
                table: "BidBonds",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BidBonds_Banks_BidBondOn_BankId",
                table: "BidBonds",
                column: "BidBondOn_BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidBonds_Banks_BidBondOn_BankId",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "BidBondOn_IsCredit",
                table: "BidBonds");

            migrationBuilder.RenameColumn(
                name: "BidBondOn_BankId",
                table: "BidBonds",
                newName: "BankId");

            migrationBuilder.RenameIndex(
                name: "IX_BidBonds_BidBondOn_BankId",
                table: "BidBonds",
                newName: "IX_BidBonds_BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_BidBonds_Banks_BankId",
                table: "BidBonds",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
