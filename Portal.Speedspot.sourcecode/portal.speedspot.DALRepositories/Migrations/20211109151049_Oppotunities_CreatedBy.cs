using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Oppotunities_CreatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Opportunities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_CreatedById",
                table: "Opportunities",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_AspNetUsers_CreatedById",
                table: "Opportunities",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_AspNetUsers_CreatedById",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_CreatedById",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Opportunities");
        }
    }
}
