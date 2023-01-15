using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Quotations_RejectReasonId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RejectReasonId",
                table: "Quotations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_RejectReasonId",
                table: "Quotations",
                column: "RejectReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_RejectReasons_RejectReasonId",
                table: "Quotations",
                column: "RejectReasonId",
                principalTable: "RejectReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_RejectReasons_RejectReasonId",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_RejectReasonId",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "RejectReasonId",
                table: "Quotations");
        }
    }
}
