using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Quotations_UpdatedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryPlaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPlaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferCertificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferCertificates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferGuarantees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferGuarantees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferValidities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferValidities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OriginDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OriginDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotationOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationId = table.Column<int>(type: "int", nullable: false),
                    SupplierOfferId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttentionTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicalSpecifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    ValidityId = table.Column<int>(type: "int", nullable: false),
                    AgainstGuaranteeId = table.Column<int>(type: "int", nullable: false),
                    LGPercentage = table.Column<int>(type: "int", nullable: true),
                    DeliveryFromDays = table.Column<int>(type: "int", nullable: false),
                    DeliveryToDays = table.Column<int>(type: "int", nullable: false),
                    DeliveryPlaceId = table.Column<int>(type: "int", nullable: false),
                    OriginDocumentId = table.Column<int>(type: "int", nullable: true),
                    DeliveryGuaranteeMonths = table.Column<int>(type: "int", nullable: true),
                    CommissionGuaranteeMonths = table.Column<int>(type: "int", nullable: true),
                    DownPaymentPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UponDeliveryPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AfterInstallationPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Factor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationOffers_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationOffers_DeliveryPlaces_DeliveryPlaceId",
                        column: x => x.DeliveryPlaceId,
                        principalTable: "DeliveryPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationOffers_OfferGuarantees_AgainstGuaranteeId",
                        column: x => x.AgainstGuaranteeId,
                        principalTable: "OfferGuarantees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationOffers_OfferValidities_ValidityId",
                        column: x => x.ValidityId,
                        principalTable: "OfferValidities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationOffers_OriginDocuments_OriginDocumentId",
                        column: x => x.OriginDocumentId,
                        principalTable: "OriginDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationOffers_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationOffers_SupplierOffers_SupplierOfferId",
                        column: x => x.SupplierOfferId,
                        principalTable: "SupplierOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuotationOfferCertificates",
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
                    table.PrimaryKey("PK_QuotationOfferCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationOfferCertificates_OfferCertificates_CertificateId",
                        column: x => x.CertificateId,
                        principalTable: "OfferCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationOfferCertificates_QuotationOffers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "QuotationOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationOfferProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationOfferId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsIncluded = table.Column<bool>(type: "bit", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationOfferProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationOfferProducts_QuotationOffers_QuotationOfferId",
                        column: x => x.QuotationOfferId,
                        principalTable: "QuotationOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationOfferProducts_SupplierOfferProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "SupplierOfferProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuotationOfferProductOrigins",
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
                    table.PrimaryKey("PK_QuotationOfferProductOrigins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationOfferProductOrigins_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationOfferProductOrigins_QuotationOfferProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "QuotationOfferProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferCertificates_CertificateId",
                table: "QuotationOfferCertificates",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferCertificates_OfferId",
                table: "QuotationOfferCertificates",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferProductOrigins_CountryId",
                table: "QuotationOfferProductOrigins",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferProductOrigins_ProductId",
                table: "QuotationOfferProductOrigins",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferProducts_ProductId",
                table: "QuotationOfferProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferProducts_QuotationOfferId",
                table: "QuotationOfferProducts",
                column: "QuotationOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOffers_AgainstGuaranteeId",
                table: "QuotationOffers",
                column: "AgainstGuaranteeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOffers_CurrencyId",
                table: "QuotationOffers",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOffers_DeliveryPlaceId",
                table: "QuotationOffers",
                column: "DeliveryPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOffers_OriginDocumentId",
                table: "QuotationOffers",
                column: "OriginDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOffers_QuotationId",
                table: "QuotationOffers",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOffers_SupplierOfferId",
                table: "QuotationOffers",
                column: "SupplierOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOffers_ValidityId",
                table: "QuotationOffers",
                column: "ValidityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuotationOfferCertificates");

            migrationBuilder.DropTable(
                name: "QuotationOfferProductOrigins");

            migrationBuilder.DropTable(
                name: "OfferCertificates");

            migrationBuilder.DropTable(
                name: "QuotationOfferProducts");

            migrationBuilder.DropTable(
                name: "QuotationOffers");

            migrationBuilder.DropTable(
                name: "DeliveryPlaces");

            migrationBuilder.DropTable(
                name: "OfferGuarantees");

            migrationBuilder.DropTable(
                name: "OfferValidities");

            migrationBuilder.DropTable(
                name: "OriginDocuments");
        }
    }
}
