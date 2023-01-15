using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class QuotationOfferProducts_RemoveIsIncluded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOffers_AspNetUsers_CreatedById",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "IsIncluded",
                table: "QuotationOfferProducts");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "QuotationOffers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "QuotationOffers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<bool>(
                name: "IsIncluded",
                table: "QuotationOfferProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOffers_AspNetUsers_CreatedById",
                table: "QuotationOffers",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
