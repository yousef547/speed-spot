using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class RenameTable_QuotationOfferVersions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferCertificates_QuotationOfferVersion_OfferId",
                table: "QuotationOfferCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferProducts_QuotationOfferVersion_OfferId",
                table: "QuotationOfferProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersion_AspNetUsers_CreatedById",
                table: "QuotationOfferVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersion_Currencies_CurrencyId",
                table: "QuotationOfferVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersion_DeliveryPlaces_DeliveryPlaceId",
                table: "QuotationOfferVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersion_OfferValidities_ValidityId",
                table: "QuotationOfferVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersion_OriginDocuments_OriginDocumentId",
                table: "QuotationOfferVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersion_QuotationOffers_OfferId",
                table: "QuotationOfferVersion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuotationOfferVersion",
                table: "QuotationOfferVersion");

            migrationBuilder.RenameTable(
                name: "QuotationOfferVersion",
                newName: "QuotationOfferVersions");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferVersion_ValidityId",
                table: "QuotationOfferVersions",
                newName: "IX_QuotationOfferVersions_ValidityId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferVersion_OriginDocumentId",
                table: "QuotationOfferVersions",
                newName: "IX_QuotationOfferVersions_OriginDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferVersion_OfferId",
                table: "QuotationOfferVersions",
                newName: "IX_QuotationOfferVersions_OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferVersion_DeliveryPlaceId",
                table: "QuotationOfferVersions",
                newName: "IX_QuotationOfferVersions_DeliveryPlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferVersion_CurrencyId",
                table: "QuotationOfferVersions",
                newName: "IX_QuotationOfferVersions_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferVersion_CreatedById",
                table: "QuotationOfferVersions",
                newName: "IX_QuotationOfferVersions_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuotationOfferVersions",
                table: "QuotationOfferVersions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferCertificates_QuotationOfferVersions_OfferId",
                table: "QuotationOfferCertificates",
                column: "OfferId",
                principalTable: "QuotationOfferVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferProducts_QuotationOfferVersions_OfferId",
                table: "QuotationOfferProducts",
                column: "OfferId",
                principalTable: "QuotationOfferVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersions_AspNetUsers_CreatedById",
                table: "QuotationOfferVersions",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersions_Currencies_CurrencyId",
                table: "QuotationOfferVersions",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersions_DeliveryPlaces_DeliveryPlaceId",
                table: "QuotationOfferVersions",
                column: "DeliveryPlaceId",
                principalTable: "DeliveryPlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersions_OfferValidities_ValidityId",
                table: "QuotationOfferVersions",
                column: "ValidityId",
                principalTable: "OfferValidities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersions_OriginDocuments_OriginDocumentId",
                table: "QuotationOfferVersions",
                column: "OriginDocumentId",
                principalTable: "OriginDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersions_QuotationOffers_OfferId",
                table: "QuotationOfferVersions",
                column: "OfferId",
                principalTable: "QuotationOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferCertificates_QuotationOfferVersions_OfferId",
                table: "QuotationOfferCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferProducts_QuotationOfferVersions_OfferId",
                table: "QuotationOfferProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersions_AspNetUsers_CreatedById",
                table: "QuotationOfferVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersions_Currencies_CurrencyId",
                table: "QuotationOfferVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersions_DeliveryPlaces_DeliveryPlaceId",
                table: "QuotationOfferVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersions_OfferValidities_ValidityId",
                table: "QuotationOfferVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersions_OriginDocuments_OriginDocumentId",
                table: "QuotationOfferVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferVersions_QuotationOffers_OfferId",
                table: "QuotationOfferVersions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuotationOfferVersions",
                table: "QuotationOfferVersions");

            migrationBuilder.RenameTable(
                name: "QuotationOfferVersions",
                newName: "QuotationOfferVersion");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferVersions_ValidityId",
                table: "QuotationOfferVersion",
                newName: "IX_QuotationOfferVersion_ValidityId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferVersions_OriginDocumentId",
                table: "QuotationOfferVersion",
                newName: "IX_QuotationOfferVersion_OriginDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferVersions_OfferId",
                table: "QuotationOfferVersion",
                newName: "IX_QuotationOfferVersion_OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferVersions_DeliveryPlaceId",
                table: "QuotationOfferVersion",
                newName: "IX_QuotationOfferVersion_DeliveryPlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferVersions_CurrencyId",
                table: "QuotationOfferVersion",
                newName: "IX_QuotationOfferVersion_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferVersions_CreatedById",
                table: "QuotationOfferVersion",
                newName: "IX_QuotationOfferVersion_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuotationOfferVersion",
                table: "QuotationOfferVersion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferCertificates_QuotationOfferVersion_OfferId",
                table: "QuotationOfferCertificates",
                column: "OfferId",
                principalTable: "QuotationOfferVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferProducts_QuotationOfferVersion_OfferId",
                table: "QuotationOfferProducts",
                column: "OfferId",
                principalTable: "QuotationOfferVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersion_AspNetUsers_CreatedById",
                table: "QuotationOfferVersion",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersion_Currencies_CurrencyId",
                table: "QuotationOfferVersion",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersion_DeliveryPlaces_DeliveryPlaceId",
                table: "QuotationOfferVersion",
                column: "DeliveryPlaceId",
                principalTable: "DeliveryPlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersion_OfferValidities_ValidityId",
                table: "QuotationOfferVersion",
                column: "ValidityId",
                principalTable: "OfferValidities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersion_OriginDocuments_OriginDocumentId",
                table: "QuotationOfferVersion",
                column: "OriginDocumentId",
                principalTable: "OriginDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferVersion_QuotationOffers_OfferId",
                table: "QuotationOfferVersion",
                column: "OfferId",
                principalTable: "QuotationOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
