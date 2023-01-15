using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class SupplierOffers_ModelUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOffers_Attachments_OfferAttachmentId",
                table: "SupplierOffers");

            migrationBuilder.DropIndex(
                name: "IX_SupplierOffers_OfferAttachmentId",
                table: "SupplierOffers");

            migrationBuilder.DropColumn(
                name: "OfferAttachmentId",
                table: "SupplierOffers");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "SupplierOffers",
                newName: "CIFCost");

            migrationBuilder.AddColumn<decimal>(
                name: "AdditionalCost",
                table: "SupplierOffers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "SupplierOfferProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    TechnicalSpecificationProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOfferProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierOfferProducts_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierOfferProducts_SupplierOffers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "SupplierOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierOfferProducts_TechnicalSpecificationProducts_TechnicalSpecificationProductId",
                        column: x => x.TechnicalSpecificationProductId,
                        principalTable: "TechnicalSpecificationProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOfferProducts_AttachmentId",
                table: "SupplierOfferProducts",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOfferProducts_OfferId",
                table: "SupplierOfferProducts",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOfferProducts_TechnicalSpecificationProductId",
                table: "SupplierOfferProducts",
                column: "TechnicalSpecificationProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierOfferProducts");

            migrationBuilder.DropColumn(
                name: "AdditionalCost",
                table: "SupplierOffers");

            migrationBuilder.RenameColumn(
                name: "CIFCost",
                table: "SupplierOffers",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "OfferAttachmentId",
                table: "SupplierOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOffers_OfferAttachmentId",
                table: "SupplierOffers",
                column: "OfferAttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOffers_Attachments_OfferAttachmentId",
                table: "SupplierOffers",
                column: "OfferAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
