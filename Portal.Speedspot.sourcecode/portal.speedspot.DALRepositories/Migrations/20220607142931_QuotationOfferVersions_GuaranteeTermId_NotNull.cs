using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class QuotationOfferVersions_GuaranteeTermId_NotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GuaranteeTermId",
                table: "QuotationOfferVersions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int?");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GuaranteeTermId",
                table: "QuotationOfferVersions",
                type: "int?",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
