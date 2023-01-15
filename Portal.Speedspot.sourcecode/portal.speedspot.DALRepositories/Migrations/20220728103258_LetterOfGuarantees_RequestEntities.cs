using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class LetterOfGuarantees_RequestEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinalLGRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    SentById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPrinted = table.Column<bool>(type: "bit", nullable: false),
                    PrintedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PrintedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalLGId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    RequestedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalLGRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalLGRequest_AspNetUsers_PrintedById",
                        column: x => x.PrintedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalLGRequest_AspNetUsers_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalLGRequest_AspNetUsers_SentById",
                        column: x => x.SentById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalLGRequest_AspNetUsers_StatusById",
                        column: x => x.StatusById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalLGRequest_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalLGRequest_LetterOfGuarantees_FinalLGId",
                        column: x => x.FinalLGId,
                        principalTable: "LetterOfGuarantees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceLGRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    SentById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPrinted = table.Column<bool>(type: "bit", nullable: false),
                    PrintedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PrintedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PerformanceLGId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    RequestedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceLGRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceLGRequest_AspNetUsers_PrintedById",
                        column: x => x.PrintedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceLGRequest_AspNetUsers_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceLGRequest_AspNetUsers_SentById",
                        column: x => x.SentById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceLGRequest_AspNetUsers_StatusById",
                        column: x => x.StatusById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceLGRequest_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceLGRequest_LetterOfGuarantees_PerformanceLGId",
                        column: x => x.PerformanceLGId,
                        principalTable: "LetterOfGuarantees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinalLGRequest_DepartmentId",
                table: "FinalLGRequest",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalLGRequest_FinalLGId",
                table: "FinalLGRequest",
                column: "FinalLGId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinalLGRequest_PrintedById",
                table: "FinalLGRequest",
                column: "PrintedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinalLGRequest_RequestedById",
                table: "FinalLGRequest",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinalLGRequest_SentById",
                table: "FinalLGRequest",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_FinalLGRequest_StatusById",
                table: "FinalLGRequest",
                column: "StatusById");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceLGRequest_DepartmentId",
                table: "PerformanceLGRequest",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceLGRequest_PerformanceLGId",
                table: "PerformanceLGRequest",
                column: "PerformanceLGId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceLGRequest_PrintedById",
                table: "PerformanceLGRequest",
                column: "PrintedById");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceLGRequest_RequestedById",
                table: "PerformanceLGRequest",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceLGRequest_SentById",
                table: "PerformanceLGRequest",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceLGRequest_StatusById",
                table: "PerformanceLGRequest",
                column: "StatusById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinalLGRequest");

            migrationBuilder.DropTable(
                name: "PerformanceLGRequest");
        }
    }
}
