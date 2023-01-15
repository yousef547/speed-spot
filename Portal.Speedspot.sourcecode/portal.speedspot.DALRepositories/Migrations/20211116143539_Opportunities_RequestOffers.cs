using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Opportunities_RequestOffers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplier_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supplier_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpportunityId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestOffer_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestOffer_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestOfferAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestOfferId = table.Column<int>(type: "int", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestOfferAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestOfferAttachment_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestOfferAttachment_RequestOffer_RequestOfferId",
                        column: x => x.RequestOfferId,
                        principalTable: "RequestOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestOfferProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestOfferId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestOfferProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestOfferProduct_RequestOffer_RequestOfferId",
                        column: x => x.RequestOfferId,
                        principalTable: "RequestOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestOfferProduct_TechnicalSpecificationProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TechnicalSpecificationProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestOffer_OpportunityId",
                table: "RequestOffer",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOffer_SupplierId",
                table: "RequestOffer",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOfferAttachment_AttachmentId",
                table: "RequestOfferAttachment",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOfferAttachment_RequestOfferId",
                table: "RequestOfferAttachment",
                column: "RequestOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOfferProduct_ProductId",
                table: "RequestOfferProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOfferProduct_RequestOfferId",
                table: "RequestOfferProduct",
                column: "RequestOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_BankId",
                table: "Supplier",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_CountryId",
                table: "Supplier",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestOfferAttachment");

            migrationBuilder.DropTable(
                name: "RequestOfferProduct");

            migrationBuilder.DropTable(
                name: "RequestOffer");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
