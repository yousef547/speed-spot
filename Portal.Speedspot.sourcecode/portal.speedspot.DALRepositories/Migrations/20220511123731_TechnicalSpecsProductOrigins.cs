using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class TechnicalSpecsProductOrigins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalSpecificationProductOrigin_Countries_CountryId",
                table: "TechnicalSpecificationProductOrigin");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalSpecificationProductOrigin_TechnicalSpecificationProducts_TechnicalSpecificationProductId",
                table: "TechnicalSpecificationProductOrigin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicalSpecificationProductOrigin",
                table: "TechnicalSpecificationProductOrigin");

            migrationBuilder.RenameTable(
                name: "TechnicalSpecificationProductOrigin",
                newName: "TechnicalSpecificationProductOrigins");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicalSpecificationProductOrigin_TechnicalSpecificationProductId",
                table: "TechnicalSpecificationProductOrigins",
                newName: "IX_TechnicalSpecificationProductOrigins_TechnicalSpecificationProductId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicalSpecificationProductOrigin_CountryId",
                table: "TechnicalSpecificationProductOrigins",
                newName: "IX_TechnicalSpecificationProductOrigins_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicalSpecificationProductOrigins",
                table: "TechnicalSpecificationProductOrigins",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalSpecificationProductOrigins_Countries_CountryId",
                table: "TechnicalSpecificationProductOrigins",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalSpecificationProductOrigins_TechnicalSpecificationProducts_TechnicalSpecificationProductId",
                table: "TechnicalSpecificationProductOrigins",
                column: "TechnicalSpecificationProductId",
                principalTable: "TechnicalSpecificationProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalSpecificationProductOrigins_Countries_CountryId",
                table: "TechnicalSpecificationProductOrigins");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalSpecificationProductOrigins_TechnicalSpecificationProducts_TechnicalSpecificationProductId",
                table: "TechnicalSpecificationProductOrigins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicalSpecificationProductOrigins",
                table: "TechnicalSpecificationProductOrigins");

            migrationBuilder.RenameTable(
                name: "TechnicalSpecificationProductOrigins",
                newName: "TechnicalSpecificationProductOrigin");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicalSpecificationProductOrigins_TechnicalSpecificationProductId",
                table: "TechnicalSpecificationProductOrigin",
                newName: "IX_TechnicalSpecificationProductOrigin_TechnicalSpecificationProductId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicalSpecificationProductOrigins_CountryId",
                table: "TechnicalSpecificationProductOrigin",
                newName: "IX_TechnicalSpecificationProductOrigin_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicalSpecificationProductOrigin",
                table: "TechnicalSpecificationProductOrigin",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalSpecificationProductOrigin_Countries_CountryId",
                table: "TechnicalSpecificationProductOrigin",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalSpecificationProductOrigin_TechnicalSpecificationProducts_TechnicalSpecificationProductId",
                table: "TechnicalSpecificationProductOrigin",
                column: "TechnicalSpecificationProductId",
                principalTable: "TechnicalSpecificationProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
