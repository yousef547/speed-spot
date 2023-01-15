using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class NegotiationResult_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NegotiationResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationId = table.Column<int>(type: "int", nullable: false),
                    OfferValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DownPaymentPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UponDeliveryPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AfterInstallationPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryFromDays = table.Column<int>(type: "int", nullable: false),
                    DeliveryToDays = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegotiationResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NegotiationResults_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtherNegotiationResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueBefore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueAfter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherNegotiationResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherNegotiationResults_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NegotiationResults_QuotationId",
                table: "NegotiationResults",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherNegotiationResults_QuotationId",
                table: "OtherNegotiationResults",
                column: "QuotationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NegotiationResults");

            migrationBuilder.DropTable(
                name: "OtherNegotiationResults");
        }
    }
}
