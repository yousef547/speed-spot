using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Suppliers_LegalInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LegalInfoId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_LegalInfoId",
                table: "Suppliers",
                column: "LegalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_LegalInfos_LegalInfoId",
                table: "Suppliers",
                column: "LegalInfoId",
                principalTable: "LegalInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_LegalInfos_LegalInfoId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_LegalInfoId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "LegalInfoId",
                table: "Suppliers");
        }
    }
}
