using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class BidBonds_BookTenders_TodoTaskId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TodoTaskId",
                table: "BookTenders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TodoTaskId",
                table: "BidBonds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookTenders_TodoTaskId",
                table: "BookTenders",
                column: "TodoTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_BidBonds_TodoTaskId",
                table: "BidBonds",
                column: "TodoTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_BidBonds_ToDoTasks_TodoTaskId",
                table: "BidBonds",
                column: "TodoTaskId",
                principalTable: "ToDoTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTenders_ToDoTasks_TodoTaskId",
                table: "BookTenders",
                column: "TodoTaskId",
                principalTable: "ToDoTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidBonds_ToDoTasks_TodoTaskId",
                table: "BidBonds");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTenders_ToDoTasks_TodoTaskId",
                table: "BookTenders");

            migrationBuilder.DropIndex(
                name: "IX_BookTenders_TodoTaskId",
                table: "BookTenders");

            migrationBuilder.DropIndex(
                name: "IX_BidBonds_TodoTaskId",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "TodoTaskId",
                table: "BookTenders");

            migrationBuilder.DropColumn(
                name: "TodoTaskId",
                table: "BidBonds");
        }
    }
}
