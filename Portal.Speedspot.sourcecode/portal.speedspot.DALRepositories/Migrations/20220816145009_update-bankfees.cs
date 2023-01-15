using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class updatebankfees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ForginExchangePercentage",
                table: "BankFees",
                newName: "ForeignExchangePercentage");

            migrationBuilder.RenameColumn(
                name: "ForginExchangeMinFees",
                table: "BankFees",
                newName: "ForeignExchangeMinFees");

            migrationBuilder.RenameColumn(
                name: "ForginExchangeCreditPercentage",
                table: "BankFees",
                newName: "ForeignExchangeCreditPercentage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ForeignExchangePercentage",
                table: "BankFees",
                newName: "ForginExchangePercentage");

            migrationBuilder.RenameColumn(
                name: "ForeignExchangeMinFees",
                table: "BankFees",
                newName: "ForginExchangeMinFees");

            migrationBuilder.RenameColumn(
                name: "ForeignExchangeCreditPercentage",
                table: "BankFees",
                newName: "ForginExchangeCreditPercentage");
        }
    }
}
