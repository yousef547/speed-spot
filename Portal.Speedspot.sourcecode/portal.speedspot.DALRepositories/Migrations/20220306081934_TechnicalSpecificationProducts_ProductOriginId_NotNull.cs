using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class TechnicalSpecificationProducts_ProductOriginId_NotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalSpecificationProducts_ProductOrigins_ProductOriginId",
                table: "TechnicalSpecificationProducts");

            migrationBuilder.AlterColumn<int>(
                name: "ProductOriginId",
                table: "TechnicalSpecificationProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "ProductOriginId",
                table: "TechnicalSpecificationProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalSpecificationProducts_ProductOrigins_ProductOriginId",
                table: "TechnicalSpecificationProducts",
                column: "ProductOriginId",
                principalTable: "ProductOrigins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
