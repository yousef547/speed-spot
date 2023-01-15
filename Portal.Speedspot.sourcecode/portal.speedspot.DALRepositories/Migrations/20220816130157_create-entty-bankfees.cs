using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class createenttybankfees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankFees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BidBondMinFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BidBondPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BidBondCreditPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerformanceLGMinFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerformanceLGPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerformanceLGCreditPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalLGMinFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalLGPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalLGCreditPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChequeCollectionMinFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChequeCollectionPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChequeCollectionCreditPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransferMinFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransferPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransferCreditPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LCMinFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LCPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LCCreditPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Form4MinFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Form4Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Form4CreditPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Form5MinFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Form5Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Form5CreditPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ForginExchangeMinFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ForginExchangePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ForginExchangeCreditPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankFees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankFees");
        }
    }
}
