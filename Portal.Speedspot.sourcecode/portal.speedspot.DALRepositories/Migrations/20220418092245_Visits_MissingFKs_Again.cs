using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Visits_MissingFKs_Again : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SalesAgentId",
                table: "Visits",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_CustomerId",
                table: "Visits",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_SalesAgentId",
                table: "Visits",
                column: "SalesAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_AspNetUsers_SalesAgentId",
                table: "Visits",
                column: "SalesAgentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Customers_CustomerId",
                table: "Visits",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_AspNetUsers_SalesAgentId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Customers_CustomerId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_CustomerId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_SalesAgentId",
                table: "Visits");

            migrationBuilder.AlterColumn<string>(
                name: "SalesAgentId",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
