using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class BookTenderRequests_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookTenders_ToDoTasks_TodoTaskId",
                table: "BookTenders");

            migrationBuilder.DropIndex(
                name: "IX_BookTenders_TodoTaskId",
                table: "BookTenders");

            migrationBuilder.DropColumn(
                name: "TodoTaskId",
                table: "BookTenders");

            migrationBuilder.AddColumn<int>(
                name: "BookTenderId",
                table: "PendingRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PendingRequests_BookTenderId",
                table: "PendingRequests",
                column: "BookTenderId",
                unique: true,
                filter: "[BookTenderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingRequests_BookTenders_BookTenderId",
                table: "PendingRequests",
                column: "BookTenderId",
                principalTable: "BookTenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingRequests_BookTenders_BookTenderId",
                table: "PendingRequests");

            migrationBuilder.DropIndex(
                name: "IX_PendingRequests_BookTenderId",
                table: "PendingRequests");

            migrationBuilder.DropColumn(
                name: "BookTenderId",
                table: "PendingRequests");

            migrationBuilder.AddColumn<int>(
                name: "TodoTaskId",
                table: "BookTenders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookTenders_TodoTaskId",
                table: "BookTenders",
                column: "TodoTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookTenders_ToDoTasks_TodoTaskId",
                table: "BookTenders",
                column: "TodoTaskId",
                principalTable: "ToDoTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
