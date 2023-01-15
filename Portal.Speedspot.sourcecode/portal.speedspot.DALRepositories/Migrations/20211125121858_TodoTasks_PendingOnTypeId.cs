using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class TodoTasks_PendingOnTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PendingOnTypeId",
                table: "ToDoTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTasks_PendingOnTypeId",
                table: "ToDoTasks",
                column: "PendingOnTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoTasks_EmployeeTypes_PendingOnTypeId",
                table: "ToDoTasks",
                column: "PendingOnTypeId",
                principalTable: "EmployeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoTasks_EmployeeTypes_PendingOnTypeId",
                table: "ToDoTasks");

            migrationBuilder.DropIndex(
                name: "IX_ToDoTasks_PendingOnTypeId",
                table: "ToDoTasks");

            migrationBuilder.DropColumn(
                name: "PendingOnTypeId",
                table: "ToDoTasks");
        }
    }
}
