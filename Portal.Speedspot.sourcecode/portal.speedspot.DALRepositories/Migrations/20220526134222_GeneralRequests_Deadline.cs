using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class GeneralRequests_Deadline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GeneralRequests_StatusById",
                table: "GeneralRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneralRequests_AspNetUsers_StatusById",
                table: "GeneralRequests");

            migrationBuilder.DropColumn(
                name: "StatusById",
                table: "GeneralRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "GeneralRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "GeneralRequests");

            migrationBuilder.AddColumn<string>(
                name: "StatusById",
                table: "GeneralRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralRequests_AspNetUsers_StatusById",
                table: "GeneralRequests",
                column: "StatusById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.CreateIndex(
                name: "IX_GeneralRequests_StatusById",
                table: "GeneralRequests",
                column: "StatusById");
        }
    }
}
