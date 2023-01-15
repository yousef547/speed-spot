using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class SupplierOffers_PaymentTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestOffer_Attachments_RequestOfferAttachmentId",
                table: "RequestOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestOffer_Opportunities_OpportunityId",
                table: "RequestOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestOffer_Supplier_SupplierId",
                table: "RequestOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestOfferProduct_RequestOffer_RequestOfferId",
                table: "RequestOfferProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestOfferProduct_TechnicalSpecificationProducts_ProductId",
                table: "RequestOfferProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Banks_BankId",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Countries_CountryId",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOffer_Attachments_OfferAttachmentId",
                table: "SupplierOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOffer_Currency_CurrencyId",
                table: "SupplierOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOffer_DeliveryType_DeliveryTypeId",
                table: "SupplierOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOffer_Opportunities_OpportunityId",
                table: "SupplierOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOffer_Supplier_SupplierId",
                table: "SupplierOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierOffer",
                table: "SupplierOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestOfferProduct",
                table: "RequestOfferProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestOffer",
                table: "RequestOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryType",
                table: "DeliveryType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currency",
                table: "Currency");

            migrationBuilder.RenameTable(
                name: "SupplierOffer",
                newName: "SupplierOffers");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "Suppliers");

            migrationBuilder.RenameTable(
                name: "RequestOfferProduct",
                newName: "RequestOfferProducts");

            migrationBuilder.RenameTable(
                name: "RequestOffer",
                newName: "RequestOffers");

            migrationBuilder.RenameTable(
                name: "DeliveryType",
                newName: "DeliveryTypes");

            migrationBuilder.RenameTable(
                name: "Currency",
                newName: "Currencies");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOffer_SupplierId",
                table: "SupplierOffers",
                newName: "IX_SupplierOffers_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOffer_OpportunityId",
                table: "SupplierOffers",
                newName: "IX_SupplierOffers_OpportunityId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOffer_OfferAttachmentId",
                table: "SupplierOffers",
                newName: "IX_SupplierOffers_OfferAttachmentId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOffer_DeliveryTypeId",
                table: "SupplierOffers",
                newName: "IX_SupplierOffers_DeliveryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOffer_CurrencyId",
                table: "SupplierOffers",
                newName: "IX_SupplierOffers_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_CountryId",
                table: "Suppliers",
                newName: "IX_Suppliers_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_BankId",
                table: "Suppliers",
                newName: "IX_Suppliers_BankId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestOfferProduct_RequestOfferId",
                table: "RequestOfferProducts",
                newName: "IX_RequestOfferProducts_RequestOfferId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestOfferProduct_ProductId",
                table: "RequestOfferProducts",
                newName: "IX_RequestOfferProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestOffer_SupplierId",
                table: "RequestOffers",
                newName: "IX_RequestOffers_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestOffer_RequestOfferAttachmentId",
                table: "RequestOffers",
                newName: "IX_RequestOffers_RequestOfferAttachmentId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestOffer_OpportunityId",
                table: "RequestOffers",
                newName: "IX_RequestOffers_OpportunityId");

            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "SupplierOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierOffers",
                table: "SupplierOffers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestOfferProducts",
                table: "RequestOfferProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestOffers",
                table: "RequestOffers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryTypes",
                table: "DeliveryTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnumValue = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOffers_PaymentTypeId",
                table: "SupplierOffers",
                column: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOfferProducts_RequestOffers_RequestOfferId",
                table: "RequestOfferProducts",
                column: "RequestOfferId",
                principalTable: "RequestOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOfferProducts_TechnicalSpecificationProducts_ProductId",
                table: "RequestOfferProducts",
                column: "ProductId",
                principalTable: "TechnicalSpecificationProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOffers_Attachments_RequestOfferAttachmentId",
                table: "RequestOffers",
                column: "RequestOfferAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOffers_Opportunities_OpportunityId",
                table: "RequestOffers",
                column: "OpportunityId",
                principalTable: "Opportunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOffers_Suppliers_SupplierId",
                table: "RequestOffers",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOffers_Attachments_OfferAttachmentId",
                table: "SupplierOffers",
                column: "OfferAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOffers_Currencies_CurrencyId",
                table: "SupplierOffers",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOffers_DeliveryTypes_DeliveryTypeId",
                table: "SupplierOffers",
                column: "DeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOffers_Opportunities_OpportunityId",
                table: "SupplierOffers",
                column: "OpportunityId",
                principalTable: "Opportunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOffers_PaymentTypes_PaymentTypeId",
                table: "SupplierOffers",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOffers_Suppliers_SupplierId",
                table: "SupplierOffers",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Banks_BankId",
                table: "Suppliers",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Countries_CountryId",
                table: "Suppliers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestOfferProducts_RequestOffers_RequestOfferId",
                table: "RequestOfferProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestOfferProducts_TechnicalSpecificationProducts_ProductId",
                table: "RequestOfferProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestOffers_Attachments_RequestOfferAttachmentId",
                table: "RequestOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestOffers_Opportunities_OpportunityId",
                table: "RequestOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestOffers_Suppliers_SupplierId",
                table: "RequestOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOffers_Attachments_OfferAttachmentId",
                table: "SupplierOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOffers_Currencies_CurrencyId",
                table: "SupplierOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOffers_DeliveryTypes_DeliveryTypeId",
                table: "SupplierOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOffers_Opportunities_OpportunityId",
                table: "SupplierOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOffers_PaymentTypes_PaymentTypeId",
                table: "SupplierOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOffers_Suppliers_SupplierId",
                table: "SupplierOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Banks_BankId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Countries_CountryId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierOffers",
                table: "SupplierOffers");

            migrationBuilder.DropIndex(
                name: "IX_SupplierOffers_PaymentTypeId",
                table: "SupplierOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestOffers",
                table: "RequestOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestOfferProducts",
                table: "RequestOfferProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryTypes",
                table: "DeliveryTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "SupplierOffers");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Supplier");

            migrationBuilder.RenameTable(
                name: "SupplierOffers",
                newName: "SupplierOffer");

            migrationBuilder.RenameTable(
                name: "RequestOffers",
                newName: "RequestOffer");

            migrationBuilder.RenameTable(
                name: "RequestOfferProducts",
                newName: "RequestOfferProduct");

            migrationBuilder.RenameTable(
                name: "DeliveryTypes",
                newName: "DeliveryType");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "Currency");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_CountryId",
                table: "Supplier",
                newName: "IX_Supplier_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_BankId",
                table: "Supplier",
                newName: "IX_Supplier_BankId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOffers_SupplierId",
                table: "SupplierOffer",
                newName: "IX_SupplierOffer_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOffers_OpportunityId",
                table: "SupplierOffer",
                newName: "IX_SupplierOffer_OpportunityId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOffers_OfferAttachmentId",
                table: "SupplierOffer",
                newName: "IX_SupplierOffer_OfferAttachmentId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOffers_DeliveryTypeId",
                table: "SupplierOffer",
                newName: "IX_SupplierOffer_DeliveryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOffers_CurrencyId",
                table: "SupplierOffer",
                newName: "IX_SupplierOffer_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestOffers_SupplierId",
                table: "RequestOffer",
                newName: "IX_RequestOffer_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestOffers_RequestOfferAttachmentId",
                table: "RequestOffer",
                newName: "IX_RequestOffer_RequestOfferAttachmentId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestOffers_OpportunityId",
                table: "RequestOffer",
                newName: "IX_RequestOffer_OpportunityId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestOfferProducts_RequestOfferId",
                table: "RequestOfferProduct",
                newName: "IX_RequestOfferProduct_RequestOfferId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestOfferProducts_ProductId",
                table: "RequestOfferProduct",
                newName: "IX_RequestOfferProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierOffer",
                table: "SupplierOffer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestOffer",
                table: "RequestOffer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestOfferProduct",
                table: "RequestOfferProduct",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryType",
                table: "DeliveryType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currency",
                table: "Currency",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOffer_Attachments_RequestOfferAttachmentId",
                table: "RequestOffer",
                column: "RequestOfferAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOffer_Opportunities_OpportunityId",
                table: "RequestOffer",
                column: "OpportunityId",
                principalTable: "Opportunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOffer_Supplier_SupplierId",
                table: "RequestOffer",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOfferProduct_RequestOffer_RequestOfferId",
                table: "RequestOfferProduct",
                column: "RequestOfferId",
                principalTable: "RequestOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOfferProduct_TechnicalSpecificationProducts_ProductId",
                table: "RequestOfferProduct",
                column: "ProductId",
                principalTable: "TechnicalSpecificationProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Banks_BankId",
                table: "Supplier",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Countries_CountryId",
                table: "Supplier",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOffer_Attachments_OfferAttachmentId",
                table: "SupplierOffer",
                column: "OfferAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOffer_Currency_CurrencyId",
                table: "SupplierOffer",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOffer_DeliveryType_DeliveryTypeId",
                table: "SupplierOffer",
                column: "DeliveryTypeId",
                principalTable: "DeliveryType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOffer_Opportunities_OpportunityId",
                table: "SupplierOffer",
                column: "OpportunityId",
                principalTable: "Opportunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOffer_Supplier_SupplierId",
                table: "SupplierOffer",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
