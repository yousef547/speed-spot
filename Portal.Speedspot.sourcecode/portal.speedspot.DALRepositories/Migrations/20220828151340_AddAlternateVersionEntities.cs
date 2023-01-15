using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class AddAlternateVersionEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlternateVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationOfferVersionId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    ValidityId = table.Column<int>(type: "int", nullable: false),
                    DeliveryFromDays = table.Column<int>(type: "int", nullable: false),
                    DeliveryToDays = table.Column<int>(type: "int", nullable: false),
                    DeliveryNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryPlaceId = table.Column<int>(type: "int", nullable: false),
                    OriginDocumentId = table.Column<int>(type: "int", nullable: true),
                    DeliveryGuaranteeMonths = table.Column<int>(type: "int", nullable: true),
                    CommissionGuaranteeMonths = table.Column<int>(type: "int", nullable: true),
                    GuaranteeTermId = table.Column<int>(type: "int", nullable: true),
                    DownPaymentPercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    UponDeliveryPercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    AfterInstallationPercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Factor = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternateVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternateVersions_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlternateVersions_DeliveryPlaces_DeliveryPlaceId",
                        column: x => x.DeliveryPlaceId,
                        principalTable: "DeliveryPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlternateVersions_GuaranteeTerms_GuaranteeTermId",
                        column: x => x.GuaranteeTermId,
                        principalTable: "GuaranteeTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlternateVersions_OfferValidities_ValidityId",
                        column: x => x.ValidityId,
                        principalTable: "OfferValidities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlternateVersions_OriginDocuments_OriginDocumentId",
                        column: x => x.OriginDocumentId,
                        principalTable: "OriginDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlternateVersionCertificates",
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
                    table.PrimaryKey("PK_AlternateVersionCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternateVersionCertificates_AlternateVersions_OfferId",
                        column: x => x.OfferId,
                        principalTable: "AlternateVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlternateVersionCertificates_OfferCertificates_CertificateId",
                        column: x => x.CertificateId,
                        principalTable: "OfferCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlternateVersionProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfferDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SupplierRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternateVersionProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternateVersionProducts_AlternateVersions_OfferId",
                        column: x => x.OfferId,
                        principalTable: "AlternateVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlternateVersionProducts_SupplierOfferProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "SupplierOfferProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlternateVersionProductOrigins",
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
                    table.PrimaryKey("PK_AlternateVersionProductOrigins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternateVersionProductOrigins_AlternateVersionProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AlternateVersionProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlternateVersionProductOrigins_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersionCertificates_CertificateId",
                table: "AlternateVersionCertificates",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersionCertificates_OfferId",
                table: "AlternateVersionCertificates",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersionProductOrigins_CountryId",
                table: "AlternateVersionProductOrigins",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersionProductOrigins_ProductId",
                table: "AlternateVersionProductOrigins",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersionProducts_OfferId",
                table: "AlternateVersionProducts",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersionProducts_ProductId",
                table: "AlternateVersionProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersions_CurrencyId",
                table: "AlternateVersions",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersions_DeliveryPlaceId",
                table: "AlternateVersions",
                column: "DeliveryPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersions_GuaranteeTermId",
                table: "AlternateVersions",
                column: "GuaranteeTermId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersions_OriginDocumentId",
                table: "AlternateVersions",
                column: "OriginDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateVersions_ValidityId",
                table: "AlternateVersions",
                column: "ValidityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlternateVersionCertificates");

            migrationBuilder.DropTable(
                name: "AlternateVersionProductOrigins");

            migrationBuilder.DropTable(
                name: "AlternateVersionProducts");

            migrationBuilder.DropTable(
                name: "AlternateVersions");
        }
    }
}
