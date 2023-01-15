using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class QuotationOfferVersions_GuaranteeTermId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuaranteeTermId",
                table: "QuotationOfferVersions",
                type: "int",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferVersions_GuaranteeTermId",
                table: "QuotationOfferVersions",
                column: "GuaranteeTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersions_GuaranteeTerms_GuaranteeTermId",
                table: "QuotationOfferVersions",
                column: "GuaranteeTermId",
                principalTable: "GuaranteeTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersions_GuaranteeTerms_GuaranteeTermId",
                table: "QuotationOfferVersions");

            migrationBuilder.DropIndex(
                name: "IX_QuotationOfferVersions_GuaranteeTermId",
                table: "QuotationOfferVersions");

            migrationBuilder.DropColumn(
                name: "GuaranteeTermId",
                table: "QuotationOfferVersions");
        }
    }
}
