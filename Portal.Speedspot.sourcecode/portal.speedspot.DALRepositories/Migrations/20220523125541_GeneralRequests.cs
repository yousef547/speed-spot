using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class GeneralRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestFromId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    RequestedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralRequests_AspNetUsers_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneralRequests_AspNetUsers_RequestFromId",
                        column: x => x.RequestFromId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneralRequests_AspNetUsers_StatusById",
                        column: x => x.StatusById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralRequests_RequestedById",
                table: "GeneralRequests",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralRequests_RequestFromId",
                table: "GeneralRequests",
                column: "RequestFromId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralRequests_StatusById",
                table: "GeneralRequests",
                column: "StatusById");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralRequests");
        }
    }
}
