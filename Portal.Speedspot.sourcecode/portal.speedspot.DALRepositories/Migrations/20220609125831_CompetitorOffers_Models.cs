using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class CompetitorOffers_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompetitorOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitorId = table.Column<int>(type: "int", nullable: false),
                    QuotationId = table.Column<int>(type: "int", nullable: false),
                    DownPaymentPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UponDeliveryPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AfterInstallationPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValidityId = table.Column<int>(type: "int", nullable: false),
                    OriginDocumentId = table.Column<int>(type: "int", nullable: true),
                    GuaranteeTermId = table.Column<int>(type: "int", nullable: false),
                    DeliveryPlaceId = table.Column<int>(type: "int", nullable: false),
                    DeliveryFromDays = table.Column<int>(type: "int", nullable: false),
                    DeliveryToDays = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsVATIncluded = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitorOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitorOffers_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitorOffers_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalTable: "Competitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitorOffers_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitorOffers_DeliveryPlaces_DeliveryPlaceId",
                        column: x => x.DeliveryPlaceId,
                        principalTable: "DeliveryPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitorOffers_GuaranteeTerms_GuaranteeTermId",
                        column: x => x.GuaranteeTermId,
                        principalTable: "GuaranteeTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitorOffers_OfferValidities_ValidityId",
                        column: x => x.ValidityId,
                        principalTable: "OfferValidities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitorOffers_OriginDocuments_OriginDocumentId",
                        column: x => x.OriginDocumentId,
                        principalTable: "OriginDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitorOffers_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitorOfferCertificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    CertificateId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitorOfferCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitorOfferCertificates_CompetitorOffers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "CompetitorOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitorOfferCertificates_OfferCertificates_CertificateId",
                        column: x => x.CertificateId,
                        principalTable: "OfferCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetitorOfferProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsIncluded = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitorOfferProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitorOfferProducts_CompetitorOffers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "CompetitorOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitorOfferProducts_TechnicalSpecificationProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TechnicalSpecificationProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetitorOfferProductOrigins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitorOfferProductOrigins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitorOfferProductOrigins_CompetitorOfferProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "CompetitorOfferProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitorOfferProductOrigins_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOfferCertificates_CertificateId",
                table: "CompetitorOfferCertificates",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOfferCertificates_OfferId",
                table: "CompetitorOfferCertificates",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOfferProductOrigins_CountryId",
                table: "CompetitorOfferProductOrigins",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOfferProductOrigins_ProductId",
                table: "CompetitorOfferProductOrigins",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOfferProducts_OfferId",
                table: "CompetitorOfferProducts",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOfferProducts_ProductId",
                table: "CompetitorOfferProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOffers_CompetitorId",
                table: "CompetitorOffers",
                column: "CompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOffers_CreatedById",
                table: "CompetitorOffers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOffers_CurrencyId",
                table: "CompetitorOffers",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOffers_DeliveryPlaceId",
                table: "CompetitorOffers",
                column: "DeliveryPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOffers_GuaranteeTermId",
                table: "CompetitorOffers",
                column: "GuaranteeTermId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOffers_OriginDocumentId",
                table: "CompetitorOffers",
                column: "OriginDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOffers_QuotationId",
                table: "CompetitorOffers",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorOffers_ValidityId",
                table: "CompetitorOffers",
                column: "ValidityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitorOfferCertificates");

            migrationBuilder.DropTable(
                name: "CompetitorOfferProductOrigins");

            migrationBuilder.DropTable(
                name: "CompetitorOfferProducts");

            migrationBuilder.DropTable(
                name: "CompetitorOffers");
        }
    }
}
