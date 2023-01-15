using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class QuotationOffers_CreatedById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "QuotationOffers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOffers_CreatedById",
                table: "QuotationOffers",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOffers_AspNetUsers_CreatedById",
                table: "QuotationOffers",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOffers_AspNetUsers_CreatedById",
                table: "QuotationOffers");

            migrationBuilder.DropIndex(
                name: "IX_QuotationOffers_CreatedById",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "QuotationOffers");
        }
    }
}
