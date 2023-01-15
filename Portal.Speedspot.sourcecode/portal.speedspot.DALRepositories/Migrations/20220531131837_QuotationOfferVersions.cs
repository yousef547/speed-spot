using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class QuotationOfferVersions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferCertificates_QuotationOffers_OfferId",
                table: "QuotationOfferCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferProducts_QuotationOffers_QuotationOfferId",
                table: "QuotationOfferProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOffers_AspNetUsers_CreatedById",
                table: "QuotationOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOffers_Currencies_CurrencyId",
                table: "QuotationOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOffers_DeliveryPlaces_DeliveryPlaceId",
                table: "QuotationOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOffers_OfferValidities_ValidityId",
                table: "QuotationOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOffers_OriginDocuments_OriginDocumentId",
                table: "QuotationOffers");

            migrationBuilder.DropIndex(
                name: "IX_QuotationOffers_CreatedById",
                table: "QuotationOffers");

            migrationBuilder.DropIndex(
                name: "IX_QuotationOffers_CurrencyId",
                table: "QuotationOffers");

            migrationBuilder.DropIndex(
                name: "IX_QuotationOffers_DeliveryPlaceId",
                table: "QuotationOffers");

            migrationBuilder.DropIndex(
                name: "IX_QuotationOffers_OriginDocumentId",
                table: "QuotationOffers");

            migrationBuilder.DropIndex(
                name: "IX_QuotationOffers_ValidityId",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "AfterInstallationPercentage",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "AttentionTo",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "AttentionTo2",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "CommissionGuaranteeMonths",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "CoverLetter",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "DeliveryFromDays",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "DeliveryGuaranteeMonths",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "DeliveryPlaceId",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "DeliveryToDays",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "DownPaymentPercentage",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Email2",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Factor",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "OriginDocumentId",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "TechnicalSpecifications",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "UponDeliveryPercentage",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "ValidityId",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "QuotationOffers");

            migrationBuilder.RenameColumn(
                name: "QuotationOfferId",
                table: "QuotationOfferProducts",
                newName: "OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferProducts_QuotationOfferId",
                table: "QuotationOfferProducts",
                newName: "IX_QuotationOfferProducts_OfferId");

            migrationBuilder.CreateTable(
                name: "QuotationOfferVersion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttentionTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttentionTo2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicalSpecifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    ValidityId = table.Column<int>(type: "int", nullable: false),
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
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsTwoEnvelopes = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationOfferVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationOfferVersion_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationOfferVersion_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationOfferVersion_DeliveryPlaces_DeliveryPlaceId",
                        column: x => x.DeliveryPlaceId,
                        principalTable: "DeliveryPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationOfferVersion_OfferValidities_ValidityId",
                        column: x => x.ValidityId,
                        principalTable: "OfferValidities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationOfferVersion_OriginDocuments_OriginDocumentId",
                        column: x => x.OriginDocumentId,
                        principalTable: "OriginDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationOfferVersion_QuotationOffers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "QuotationOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferVersion_CreatedById",
                table: "QuotationOfferVersion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferVersion_CurrencyId",
                table: "QuotationOfferVersion",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferVersion_DeliveryPlaceId",
                table: "QuotationOfferVersion",
                column: "DeliveryPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferVersion_OfferId",
                table: "QuotationOfferVersion",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferVersion_OriginDocumentId",
                table: "QuotationOfferVersion",
                column: "OriginDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOfferVersion_ValidityId",
                table: "QuotationOfferVersion",
                column: "ValidityId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferCertificates_QuotationOfferVersion_OfferId",
                table: "QuotationOfferCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOfferProducts_QuotationOfferVersion_OfferId",
                table: "QuotationOfferProducts");

            migrationBuilder.DropTable(
                name: "QuotationOfferVersion");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "QuotationOfferProducts",
                newName: "QuotationOfferId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationOfferProducts_OfferId",
                table: "QuotationOfferProducts",
                newName: "IX_QuotationOfferProducts_QuotationOfferId");

            migrationBuilder.AddColumn<decimal>(
                name: "AfterInstallationPercentage",
                table: "QuotationOffers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "AttentionTo",
                table: "QuotationOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AttentionTo2",
                table: "QuotationOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CommissionGuaranteeMonths",
                table: "QuotationOffers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverLetter",
                table: "QuotationOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "QuotationOffers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "QuotationOffers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "QuotationOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryFromDays",
                table: "QuotationOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryGuaranteeMonths",
                table: "QuotationOffers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryPlaceId",
                table: "QuotationOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryToDays",
                table: "QuotationOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DownPaymentPercentage",
                table: "QuotationOffers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "QuotationOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email2",
                table: "QuotationOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Factor",
                table: "QuotationOffers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "QuotationOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OriginDocumentId",
                table: "QuotationOffers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "QuotationOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone2",
                table: "QuotationOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TechnicalSpecifications",
                table: "QuotationOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "QuotationOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "UponDeliveryPercentage",
                table: "QuotationOffers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ValidityId",
                table: "QuotationOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "QuotationOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOffers_CreatedById",
                table: "QuotationOffers",
                column: "CreatedById");

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
                name: "IX_QuotationOffers_ValidityId",
                table: "QuotationOffers",
                column: "ValidityId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferCertificates_QuotationOffers_OfferId",
                table: "QuotationOfferCertificates",
                column: "OfferId",
                principalTable: "QuotationOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOfferProducts_QuotationOffers_QuotationOfferId",
                table: "QuotationOfferProducts",
                column: "QuotationOfferId",
                principalTable: "QuotationOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOffers_AspNetUsers_CreatedById",
                table: "QuotationOffers",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOffers_Currencies_CurrencyId",
                table: "QuotationOffers",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOffers_DeliveryPlaces_DeliveryPlaceId",
                table: "QuotationOffers",
                column: "DeliveryPlaceId",
                principalTable: "DeliveryPlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOffers_OfferValidities_ValidityId",
                table: "QuotationOffers",
                column: "ValidityId",
                principalTable: "OfferValidities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOffers_OriginDocuments_OriginDocumentId",
                table: "QuotationOffers",
                column: "OriginDocumentId",
                principalTable: "OriginDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
