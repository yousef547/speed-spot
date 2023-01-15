using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class RefactorQuotationOfferProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOffers_SupplierOffers_SupplierOfferId",
                table: "QuotationOffers");

            migrationBuilder.DropIndex(
                name: "IX_QuotationOffers_SupplierOfferId",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "SupplierOfferId",
                table: "QuotationOffers");

            migrationBuilder.AddColumn<decimal>(
                name: "SupplierPrice",
                table: "QuotationOfferProducts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SupplierRate",
                table: "QuotationOfferProducts",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierPrice",
                table: "QuotationOfferProducts");

            migrationBuilder.DropColumn(
                name: "SupplierRate",
                table: "QuotationOfferProducts");

            migrationBuilder.AddColumn<int>(
                name: "SupplierOfferId",
                table: "QuotationOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOffers_SupplierOfferId",
                table: "QuotationOffers",
                column: "SupplierOfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOffers_SupplierOffers_SupplierOfferId",
                table: "QuotationOffers",
                column: "SupplierOfferId",
                principalTable: "SupplierOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
