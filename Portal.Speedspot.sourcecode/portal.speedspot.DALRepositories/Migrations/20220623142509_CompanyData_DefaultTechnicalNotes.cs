using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class CompanyData_DefaultTechnicalNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultOfferTechnicalNotes",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DefaultOfferTechnicalNotesAr",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultOfferTechnicalNotes",
                table: "CompanyData");

            migrationBuilder.DropColumn(
                name: "DefaultOfferTechnicalNotesAr",
                table: "CompanyData");
        }
    }
}
