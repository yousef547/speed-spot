using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class RequestOffers_ModelUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestOffers_Attachments_RequestOfferAttachmentId",
                table: "RequestOffers");

            migrationBuilder.DropIndex(
                name: "IX_RequestOffers_RequestOfferAttachmentId",
                table: "RequestOffers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RequestOffers");

            migrationBuilder.DropColumn(
                name: "RequestOfferAttachmentId",
                table: "RequestOffers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RequestOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestOfferAttachmentId",
                table: "RequestOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RequestOffers_RequestOfferAttachmentId",
                table: "RequestOffers",
                column: "RequestOfferAttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOffers_Attachments_RequestOfferAttachmentId",
                table: "RequestOffers",
                column: "RequestOfferAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
