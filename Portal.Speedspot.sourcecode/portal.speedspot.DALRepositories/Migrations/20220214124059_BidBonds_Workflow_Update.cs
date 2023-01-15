using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class BidBonds_Workflow_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidBonds_AspNetUsers_ApprovedById",
                table: "BidBonds");

            migrationBuilder.DropForeignKey(
                name: "FK_BidBonds_AspNetUsers_SentById",
                table: "BidBonds");

            migrationBuilder.DropForeignKey(
                name: "FK_BidBonds_ToDoTasks_TodoTaskId",
                table: "BidBonds");

            migrationBuilder.DropIndex(
                name: "IX_BidBonds_ApprovedById",
                table: "BidBonds");

            migrationBuilder.DropIndex(
                name: "IX_BidBonds_SentById",
                table: "BidBonds");

            migrationBuilder.DropIndex(
                name: "IX_BidBonds_TodoTaskId",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "IsRequested",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "SentById",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "TodoTaskId",
                table: "BidBonds");

            migrationBuilder.CreateTable(
                name: "PendingRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BidBondId = table.Column<int>(type: "int", nullable: true),
                    RequestedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSent = table.Column<bool>(type: "bit", nullable: true),
                    SentById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    StatusById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PendingRequests_AspNetUsers_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PendingRequests_AspNetUsers_SentById",
                        column: x => x.SentById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PendingRequests_AspNetUsers_StatusById",
                        column: x => x.StatusById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PendingRequests_BidBonds_BidBondId",
                        column: x => x.BidBondId,
                        principalTable: "BidBonds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PendingRequests_BidBondId",
                table: "PendingRequests",
                column: "BidBondId",
                unique: true,
                filter: "[BidBondId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PendingRequests_RequestedById",
                table: "PendingRequests",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_PendingRequests_SentById",
                table: "PendingRequests",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_PendingRequests_StatusById",
                table: "PendingRequests",
                column: "StatusById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingRequests");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "BidBonds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "BidBonds",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequested",
                table: "BidBonds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "BidBonds",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SentById",
                table: "BidBonds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TodoTaskId",
                table: "BidBonds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BidBonds_ApprovedById",
                table: "BidBonds",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_BidBonds_SentById",
                table: "BidBonds",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_BidBonds_TodoTaskId",
                table: "BidBonds",
                column: "TodoTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_BidBonds_AspNetUsers_ApprovedById",
                table: "BidBonds",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BidBonds_AspNetUsers_SentById",
                table: "BidBonds",
                column: "SentById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BidBonds_ToDoTasks_TodoTaskId",
                table: "BidBonds",
                column: "TodoTaskId",
                principalTable: "ToDoTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
