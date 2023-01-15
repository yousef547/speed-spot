using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class UpdateQuotationOfferVersionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AlternateVersions_QuotationOfferVersionId",
                table: "AlternateVersions");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersions_QuotationOfferVersionId",
                table: "AlternateVersions",
                column: "QuotationOfferVersionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AlternateVersions_QuotationOfferVersionId",
                table: "AlternateVersions");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersions_QuotationOfferVersionId",
                table: "AlternateVersions",
                column: "QuotationOfferVersionId",
                unique: true);
        }
    }
}
