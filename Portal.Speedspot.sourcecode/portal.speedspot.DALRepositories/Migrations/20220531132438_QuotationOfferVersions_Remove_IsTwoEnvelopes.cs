using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class QuotationOfferVersions_Remove_IsTwoEnvelopes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTwoEnvelopes",
                table: "QuotationOfferVersion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTwoEnvelopes",
                table: "QuotationOfferVersion",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
