using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class BidbondRequests_PrintedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrinted",
                table: "PendingRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrintedById",
                table: "PendingRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PrintedDate",
                table: "PendingRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PendingRequests_PrintedById",
                table: "PendingRequests",
                column: "PrintedById");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingRequests_AspNetUsers_PrintedById",
                table: "PendingRequests",
                column: "PrintedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingRequests_AspNetUsers_PrintedById",
                table: "PendingRequests");

            migrationBuilder.DropIndex(
                name: "IX_PendingRequests_PrintedById",
                table: "PendingRequests");

            migrationBuilder.DropColumn(
                name: "IsPrinted",
                table: "PendingRequests");

            migrationBuilder.DropColumn(
                name: "PrintedById",
                table: "PendingRequests");

            migrationBuilder.DropColumn(
                name: "PrintedDate",
                table: "PendingRequests");
        }
    }
}
