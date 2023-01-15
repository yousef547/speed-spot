using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class AddCreatedByToPaymentRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "PaymentRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_CreatedByUserId",
                table: "PaymentRequests",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_AspNetUsers_CreatedByUserId",
                table: "PaymentRequests",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_AspNetUsers_CreatedByUserId",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_CreatedByUserId",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "PaymentRequests");
        }
    }
}
