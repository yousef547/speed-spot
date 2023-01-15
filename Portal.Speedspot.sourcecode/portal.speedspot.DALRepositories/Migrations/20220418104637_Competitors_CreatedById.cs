using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Competitors_CreatedById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Competitors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_CreatedById",
                table: "Competitors",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitors_AspNetUsers_CreatedById",
                table: "Competitors",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitors_AspNetUsers_CreatedById",
                table: "Competitors");

            migrationBuilder.DropIndex(
                name: "IX_Competitors_CreatedById",
                table: "Competitors");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Competitors");
        }
    }
}
