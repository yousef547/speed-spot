using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Banks_CountryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Banks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CountryId",
                table: "Banks",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Countries_CountryId",
                table: "Banks",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banks_Countries_CountryId",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Banks_CountryId",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Banks");
        }
    }
}
