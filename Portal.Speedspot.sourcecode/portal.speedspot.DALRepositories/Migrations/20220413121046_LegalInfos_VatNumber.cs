using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class LegalInfos_VatNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VAT",
                table: "LegalInfos");

            migrationBuilder.AddColumn<string>(
                name: "VatNumber",
                table: "LegalInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VatNumber",
                table: "LegalInfos");

            migrationBuilder.AddColumn<decimal>(
                name: "VAT",
                table: "LegalInfos",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
