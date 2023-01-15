using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Opportunities_NewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLimitedTenderType",
                table: "Opportunities",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenderNumber",
                table: "Opportunities",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLimitedTenderType",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "TenderNumber",
                table: "Opportunities");
        }
    }
}
