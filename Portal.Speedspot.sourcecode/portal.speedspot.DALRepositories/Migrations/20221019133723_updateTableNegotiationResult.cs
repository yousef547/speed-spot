using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class updateTableNegotiationResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedAlternateId",
                table: "NegotiationResults",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelectedVersionId",
                table: "NegotiationResults",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NegotiationResults_SelectedAlternateId",
                table: "NegotiationResults",
                column: "SelectedAlternateId");

            migrationBuilder.CreateIndex(
                name: "IX_NegotiationResults_SelectedVersionId",
                table: "NegotiationResults",
                column: "SelectedVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_NegotiationResults_AlternateVersions_SelectedAlternateId",
                table: "NegotiationResults",
                column: "SelectedAlternateId",
                principalTable: "AlternateVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NegotiationResults_QuotationOfferVersions_SelectedVersionId",
                table: "NegotiationResults",
                column: "SelectedVersionId",
                principalTable: "QuotationOfferVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NegotiationResults_AlternateVersions_SelectedAlternateId",
                table: "NegotiationResults");

            migrationBuilder.DropForeignKey(
                name: "FK_NegotiationResults_QuotationOfferVersions_SelectedVersionId",
                table: "NegotiationResults");

            migrationBuilder.DropIndex(
                name: "IX_NegotiationResults_SelectedAlternateId",
                table: "NegotiationResults");

            migrationBuilder.DropIndex(
                name: "IX_NegotiationResults_SelectedVersionId",
                table: "NegotiationResults");

            migrationBuilder.DropColumn(
                name: "SelectedAlternateId",
                table: "NegotiationResults");

            migrationBuilder.DropColumn(
                name: "SelectedVersionId",
                table: "NegotiationResults");
        }
    }
}
