using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class QuotationOffers_NewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttentionTo2",
                table: "QuotationOffers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

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
                nullable: true,
                defaultValue: "");

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
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "QuotationOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttentionTo2",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Email2",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "QuotationOffers");
        }
    }
}
