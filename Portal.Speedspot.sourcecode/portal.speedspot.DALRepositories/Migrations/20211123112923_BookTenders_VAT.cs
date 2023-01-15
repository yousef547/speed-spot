using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class BookTenders_VAT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "VAT",
                table: "BookTenders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 14m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VAT",
                table: "BookTenders");
        }
    }
}
