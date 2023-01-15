using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class QuotationAcceptedCompetitors_Rename_QuotationCompetitors_IsTechnicalAccepted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuotationAcceptedCompetitors");

            migrationBuilder.CreateTable(
                name: "QuotationCompetitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationId = table.Column<int>(type: "int", nullable: false),
                    CompetitorId = table.Column<int>(type: "int", nullable: false),
                    IsTechnicalAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationCompetitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationCompetitors_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalTable: "Competitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationCompetitors_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotationCompetitors_CompetitorId",
                table: "QuotationCompetitors",
                column: "CompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationCompetitors_QuotationId",
                table: "QuotationCompetitors",
                column: "QuotationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuotationCompetitors");

            migrationBuilder.CreateTable(
                name: "QuotationAcceptedCompetitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitorId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    QuotationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationAcceptedCompetitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationAcceptedCompetitors_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalTable: "Competitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationAcceptedCompetitors_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotationAcceptedCompetitors_CompetitorId",
                table: "QuotationAcceptedCompetitors",
                column: "CompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationAcceptedCompetitors_QuotationId",
                table: "QuotationAcceptedCompetitors",
                column: "QuotationId");
        }
    }
}
