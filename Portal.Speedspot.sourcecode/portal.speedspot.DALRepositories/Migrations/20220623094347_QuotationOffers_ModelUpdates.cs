using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class QuotationOffers_ModelUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersions_GuaranteeTerms_GuaranteeTermId",
                table: "QuotationOfferVersions");

            migrationBuilder.AlterColumn<int>(
                name: "GuaranteeTermId",
                table: "QuotationOfferVersions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "TechnicalNotes",
                table: "QuotationOfferVersions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfferDescription",
                table: "QuotationOfferProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.DropColumn(
                name: "TechnicalNotes",
                table: "QuotationOfferVersions");

            migrationBuilder.DropColumn(
                name: "OfferDescription",
                table: "QuotationOfferProducts");

            migrationBuilder.AlterColumn<int>(
                name: "GuaranteeTermId",
                table: "QuotationOfferVersions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersions_GuaranteeTerms_GuaranteeTermId",
                table: "QuotationOfferVersions",
                column: "GuaranteeTermId",
                principalTable: "GuaranteeTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
