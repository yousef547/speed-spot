using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class AddTechSpecsToAlternateVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TechnicalSpecifications",
                table: "AlternateVersions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersions_QuotationOfferVersionId",
                table: "AlternateVersions",
                column: "QuotationOfferVersionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AlternateVersions_QuotationOfferVersions_QuotationOfferVersionId",
                table: "AlternateVersions",
                column: "QuotationOfferVersionId",
                principalTable: "QuotationOfferVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlternateVersions_QuotationOfferVersions_QuotationOfferVersionId",
                table: "AlternateVersions");

            migrationBuilder.DropIndex(
                name: "IX_AlternateVersions_QuotationOfferVersionId",
                table: "AlternateVersions");

            migrationBuilder.DropColumn(
                name: "TechnicalSpecifications",
                table: "AlternateVersions");
        }
    }
}
