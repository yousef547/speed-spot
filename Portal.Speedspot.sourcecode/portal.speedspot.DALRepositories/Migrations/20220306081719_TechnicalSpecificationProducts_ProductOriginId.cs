using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class TechnicalSpecificationProducts_ProductOriginId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductOriginId",
                table: "TechnicalSpecificationProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalSpecificationProducts_ProductOriginId",
                table: "TechnicalSpecificationProducts",
                column: "ProductOriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalSpecificationProducts_ProductOrigins_ProductOriginId",
                table: "TechnicalSpecificationProducts",
                column: "ProductOriginId",
                principalTable: "ProductOrigins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalSpecificationProducts_ProductOrigins_ProductOriginId",
                table: "TechnicalSpecificationProducts");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalSpecificationProducts_ProductOriginId",
                table: "TechnicalSpecificationProducts");

            migrationBuilder.DropColumn(
                name: "ProductOriginId",
                table: "TechnicalSpecificationProducts");
        }
    }
}
