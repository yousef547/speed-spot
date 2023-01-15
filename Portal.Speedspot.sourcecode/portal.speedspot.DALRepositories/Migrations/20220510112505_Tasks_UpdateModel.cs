using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Tasks_UpdateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_ApprovedById",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_EmployeeTypes_PendingOnTypeId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "ToDoTasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ApprovedById",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_PendingOnTypeId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "PendingOnTypeId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "priorityLevels",
                table: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriorityLevel",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriorityLevel",
                table: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PendingOnTypeId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "priorityLevels",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ToDoTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AssignedToId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoneDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    IsRejected = table.Column<bool>(type: "bit", nullable: false),
                    PendingOnTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoTasks_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ToDoTasks_AspNetUsers_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToDoTasks_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToDoTasks_EmployeeTypes_PendingOnTypeId",
                        column: x => x.PendingOnTypeId,
                        principalTable: "EmployeeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ApprovedById",
                table: "Tasks",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PendingOnTypeId",
                table: "Tasks",
                column: "PendingOnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTasks_ApprovedById",
                table: "ToDoTasks",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTasks_AssignedToId",
                table: "ToDoTasks",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTasks_CreatedById",
                table: "ToDoTasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTasks_PendingOnTypeId",
                table: "ToDoTasks",
                column: "PendingOnTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_ApprovedById",
                table: "Tasks",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_EmployeeTypes_PendingOnTypeId",
                table: "Tasks",
                column: "PendingOnTypeId",
                principalTable: "EmployeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
