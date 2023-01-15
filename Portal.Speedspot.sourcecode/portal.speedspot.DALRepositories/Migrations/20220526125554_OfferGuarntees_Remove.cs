using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class OfferGuarntees_Remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralRequests_AspNetUsers_RequestFromId",
                table: "GeneralRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationOffers_OfferGuarantees_AgainstGuaranteeId",
                table: "QuotationOffers");

            migrationBuilder.DropTable(
                name: "OfferGuarantees");

            migrationBuilder.DropIndex(
                name: "IX_QuotationOffers_AgainstGuaranteeId",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "AgainstGuaranteeId",
                table: "QuotationOffers");

            migrationBuilder.DropColumn(
                name: "LGPercentage",
                table: "QuotationOffers");

            migrationBuilder.AlterColumn<string>(
                name: "RequestFromId",
                table: "GeneralRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralRequests_AspNetUsers_RequestFromId",
                table: "GeneralRequests",
                column: "RequestFromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralRequests_AspNetUsers_RequestFromId",
                table: "GeneralRequests");

            migrationBuilder.AddColumn<int>(
                name: "AgainstGuaranteeId",
                table: "QuotationOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LGPercentage",
                table: "QuotationOffers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestFromId",
                table: "GeneralRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "OfferGuarantees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferGuarantees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotationOffers_AgainstGuaranteeId",
                table: "QuotationOffers",
                column: "AgainstGuaranteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralRequests_AspNetUsers_RequestFromId",
                table: "GeneralRequests",
                column: "RequestFromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationOffers_OfferGuarantees_AgainstGuaranteeId",
                table: "QuotationOffers",
                column: "AgainstGuaranteeId",
                principalTable: "OfferGuarantees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
