using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class updatetablebank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BidBondFees",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "BidBondPercentage",
                table: "Banks");

            migrationBuilder.AddColumn<int>(
                name: "bankId",
                table: "BankFees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BankFees_bankId",
                table: "BankFees",
                column: "bankId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BankFees_Banks_bankId",
                table: "BankFees",
                column: "bankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankFees_Banks_bankId",
                table: "BankFees");

            migrationBuilder.DropIndex(
                name: "IX_BankFees_bankId",
                table: "BankFees");

            migrationBuilder.DropColumn(
                name: "bankId",
                table: "BankFees");

            migrationBuilder.AddColumn<decimal>(
                name: "BidBondFees",
                table: "Banks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BidBondPercentage",
                table: "Banks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
