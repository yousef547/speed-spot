using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Opportunities_AdditionalColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConvertedById",
                table: "Opportunities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsConverted",
                table: "Opportunities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalApprovedById",
                table: "Opportunities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_ConvertedById",
                table: "Opportunities",
                column: "ConvertedById");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_TechnicalApprovedById",
                table: "Opportunities",
                column: "TechnicalApprovedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_AspNetUsers_ConvertedById",
                table: "Opportunities",
                column: "ConvertedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_AspNetUsers_TechnicalApprovedById",
                table: "Opportunities",
                column: "TechnicalApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_AspNetUsers_ConvertedById",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_AspNetUsers_TechnicalApprovedById",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_ConvertedById",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_TechnicalApprovedById",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "ConvertedById",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "IsConverted",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "TechnicalApprovedById",
                table: "Opportunities");
        }
    }
}
