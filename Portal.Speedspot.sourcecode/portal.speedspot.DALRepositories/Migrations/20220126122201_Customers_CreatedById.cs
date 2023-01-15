using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Customers_CreatedById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedById",
                table: "Customers",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_CreatedById",
                table: "Customers",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_CreatedById",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CreatedById",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Customers");
        }
    }
}
