using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Quotations_NewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LGType",
                table: "Quotations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LetterOfGuaranteeNo",
                table: "LetterOfGuarantees",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LGType",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "LetterOfGuaranteeNo",
                table: "LetterOfGuarantees");
        }
    }
}
