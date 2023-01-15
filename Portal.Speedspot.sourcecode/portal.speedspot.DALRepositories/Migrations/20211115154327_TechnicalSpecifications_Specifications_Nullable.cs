using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class TechnicalSpecifications_Specifications_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TechnicalSpecifications_OpportunityId",
                table: "TechnicalSpecifications");

            migrationBuilder.AlterColumn<string>(
                name: "Specifications",
                table: "TechnicalSpecifications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalSpecifications_OpportunityId",
                table: "TechnicalSpecifications",
                column: "OpportunityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TechnicalSpecifications_OpportunityId",
                table: "TechnicalSpecifications");

            migrationBuilder.AlterColumn<string>(
                name: "Specifications",
                table: "TechnicalSpecifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalSpecifications_OpportunityId",
                table: "TechnicalSpecifications",
                column: "OpportunityId");
        }
    }
}
