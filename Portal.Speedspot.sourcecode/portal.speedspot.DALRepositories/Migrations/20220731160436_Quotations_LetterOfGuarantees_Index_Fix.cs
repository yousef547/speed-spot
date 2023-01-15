using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Quotations_LetterOfGuarantees_Index_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Quotations_FinalLGId",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_PerformanceLGId",
                table: "Quotations");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_FinalLGId",
                table: "Quotations",
                column: "FinalLGId",
                unique: true,
                filter: "[FinalLGId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_PerformanceLGId",
                table: "Quotations",
                column: "PerformanceLGId",
                unique: true,
                filter: "[PerformanceLGId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Quotations_FinalLGId",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_PerformanceLGId",
                table: "Quotations");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_FinalLGId",
                table: "Quotations",
                column: "FinalLGId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_PerformanceLGId",
                table: "Quotations",
                column: "PerformanceLGId");
        }
    }
}
