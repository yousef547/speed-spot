using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class TechnicalSpecificationProductOrigins : Migration
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
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "TechnicalSpecificationProductOrigin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnicalSpecificationProductId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalSpecificationProductOrigin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalSpecificationProductOrigin_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TechnicalSpecificationProductOrigin_TechnicalSpecificationProducts_TechnicalSpecificationProductId",
                        column: x => x.TechnicalSpecificationProductId,
                        principalTable: "TechnicalSpecificationProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalSpecificationProductOrigin_CountryId",
                table: "TechnicalSpecificationProductOrigin",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalSpecificationProductOrigin_TechnicalSpecificationProductId",
                table: "TechnicalSpecificationProductOrigin",
                column: "TechnicalSpecificationProductId");

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

            migrationBuilder.DropTable(
                name: "TechnicalSpecificationProductOrigin");

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
