using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class RequestOffers_Attachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestOfferAttachment");

            migrationBuilder.AddColumn<int>(
                name: "RequestOfferAttachmentId",
                table: "RequestOffer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RequestOffer_RequestOfferAttachmentId",
                table: "RequestOffer",
                column: "RequestOfferAttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOffer_Attachments_RequestOfferAttachmentId",
                table: "RequestOffer",
                column: "RequestOfferAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestOffer_Attachments_RequestOfferAttachmentId",
                table: "RequestOffer");

            migrationBuilder.DropIndex(
                name: "IX_RequestOffer_RequestOfferAttachmentId",
                table: "RequestOffer");

            migrationBuilder.DropColumn(
                name: "RequestOfferAttachmentId",
                table: "RequestOffer");

            migrationBuilder.CreateTable(
                name: "RequestOfferAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttachmentId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    RequestOfferId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_RequestOfferAttachment_AttachmentId",
                table: "RequestOfferAttachment",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOfferAttachment_RequestOfferId",
                table: "RequestOfferAttachment",
                column: "RequestOfferId");
        }
    }
}
