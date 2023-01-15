using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Quotations_LetterOfGuarantees_ReverseFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LetterOfGuarantees_Quotations_QuotationId",
                table: "LetterOfGuarantees");

            migrationBuilder.DropIndex(
                name: "IX_LetterOfGuarantees_QuotationId",
                table: "LetterOfGuarantees");

            migrationBuilder.DropColumn(
                name: "QuotationId",
                table: "LetterOfGuarantees");

            migrationBuilder.AddColumn<int>(
                name: "FinalLGId",
                table: "Quotations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PerformanceLGId",
                table: "Quotations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_FinalLGId",
                table: "Quotations",
                column: "FinalLGId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_PerformanceLGId",
                table: "Quotations",
                column: "PerformanceLGId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_LetterOfGuarantees_FinalLGId",
                table: "Quotations",
                column: "FinalLGId",
                principalTable: "LetterOfGuarantees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_LetterOfGuarantees_PerformanceLGId",
                table: "Quotations",
                column: "PerformanceLGId",
                principalTable: "LetterOfGuarantees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_LetterOfGuarantees_FinalLGId",
                table: "Quotations");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_LetterOfGuarantees_PerformanceLGId",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_FinalLGId",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_PerformanceLGId",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "FinalLGId",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "PerformanceLGId",
                table: "Quotations");

            migrationBuilder.AddColumn<int>(
                name: "QuotationId",
                table: "LetterOfGuarantees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LetterOfGuarantees_QuotationId",
                table: "LetterOfGuarantees",
                column: "QuotationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LetterOfGuarantees_Quotations_QuotationId",
                table: "LetterOfGuarantees",
                column: "QuotationId",
                principalTable: "Quotations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
