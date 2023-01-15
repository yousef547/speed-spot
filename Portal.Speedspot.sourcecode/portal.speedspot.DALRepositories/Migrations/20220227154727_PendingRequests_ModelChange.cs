using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class PendingRequests_ModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingRequests_AspNetUsers_PrintedById",
                table: "PendingRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingRequests_AspNetUsers_RequestedById",
                table: "PendingRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingRequests_AspNetUsers_SentById",
                table: "PendingRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingRequests_AspNetUsers_StatusById",
                table: "PendingRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingRequests_BidBonds_BidBondId",
                table: "PendingRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingRequests_BookTenders_BookTenderId",
                table: "PendingRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PendingRequests",
                table: "PendingRequests");

            migrationBuilder.DropIndex(
                name: "IX_PendingRequests_BidBondId",
                table: "PendingRequests");

            migrationBuilder.DropIndex(
                name: "IX_PendingRequests_BookTenderId",
                table: "PendingRequests");

            migrationBuilder.DropColumn(
                name: "BidBondId",
                table: "PendingRequests");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "PendingRequests");

            migrationBuilder.RenameTable(
                name: "PendingRequests",
                newName: "BookTenderRequests");

            migrationBuilder.RenameIndex(
                name: "IX_PendingRequests_StatusById",
                table: "BookTenderRequests",
                newName: "IX_BookTenderRequests_StatusById");

            migrationBuilder.RenameIndex(
                name: "IX_PendingRequests_SentById",
                table: "BookTenderRequests",
                newName: "IX_BookTenderRequests_SentById");

            migrationBuilder.RenameIndex(
                name: "IX_PendingRequests_RequestedById",
                table: "BookTenderRequests",
                newName: "IX_BookTenderRequests_RequestedById");

            migrationBuilder.RenameIndex(
                name: "IX_PendingRequests_PrintedById",
                table: "BookTenderRequests",
                newName: "IX_BookTenderRequests_PrintedById");

            migrationBuilder.AlterColumn<int>(
                name: "BookTenderId",
                table: "BookTenderRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookTenderRequests",
                table: "BookTenderRequests",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BidBondRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BidBondId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    RequestedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    SentById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPrinted = table.Column<bool>(type: "bit", nullable: false),
                    PrintedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PrintedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidBondRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidBondRequests_AspNetUsers_PrintedById",
                        column: x => x.PrintedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BidBondRequests_AspNetUsers_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BidBondRequests_AspNetUsers_SentById",
                        column: x => x.SentById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BidBondRequests_AspNetUsers_StatusById",
                        column: x => x.StatusById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BidBondRequests_BidBonds_BidBondId",
                        column: x => x.BidBondId,
                        principalTable: "BidBonds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookTenderRequests_BookTenderId",
                table: "BookTenderRequests",
                column: "BookTenderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BidBondRequests_BidBondId",
                table: "BidBondRequests",
                column: "BidBondId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BidBondRequests_PrintedById",
                table: "BidBondRequests",
                column: "PrintedById");

            migrationBuilder.CreateIndex(
                name: "IX_BidBondRequests_RequestedById",
                table: "BidBondRequests",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_BidBondRequests_SentById",
                table: "BidBondRequests",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_BidBondRequests_StatusById",
                table: "BidBondRequests",
                column: "StatusById");

            migrationBuilder.AddForeignKey(
                name: "FK_BookTenderRequests_AspNetUsers_PrintedById",
                table: "BookTenderRequests",
                column: "PrintedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTenderRequests_AspNetUsers_RequestedById",
                table: "BookTenderRequests",
                column: "RequestedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTenderRequests_AspNetUsers_SentById",
                table: "BookTenderRequests",
                column: "SentById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTenderRequests_AspNetUsers_StatusById",
                table: "BookTenderRequests",
                column: "StatusById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTenderRequests_BookTenders_BookTenderId",
                table: "BookTenderRequests",
                column: "BookTenderId",
                principalTable: "BookTenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookTenderRequests_AspNetUsers_PrintedById",
                table: "BookTenderRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTenderRequests_AspNetUsers_RequestedById",
                table: "BookTenderRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTenderRequests_AspNetUsers_SentById",
                table: "BookTenderRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTenderRequests_AspNetUsers_StatusById",
                table: "BookTenderRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTenderRequests_BookTenders_BookTenderId",
                table: "BookTenderRequests");

            migrationBuilder.DropTable(
                name: "BidBondRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookTenderRequests",
                table: "BookTenderRequests");

            migrationBuilder.DropIndex(
                name: "IX_BookTenderRequests_BookTenderId",
                table: "BookTenderRequests");

            migrationBuilder.RenameTable(
                name: "BookTenderRequests",
                newName: "PendingRequests");

            migrationBuilder.RenameIndex(
                name: "IX_BookTenderRequests_StatusById",
                table: "PendingRequests",
                newName: "IX_PendingRequests_StatusById");

            migrationBuilder.RenameIndex(
                name: "IX_BookTenderRequests_SentById",
                table: "PendingRequests",
                newName: "IX_PendingRequests_SentById");

            migrationBuilder.RenameIndex(
                name: "IX_BookTenderRequests_RequestedById",
                table: "PendingRequests",
                newName: "IX_PendingRequests_RequestedById");

            migrationBuilder.RenameIndex(
                name: "IX_BookTenderRequests_PrintedById",
                table: "PendingRequests",
                newName: "IX_PendingRequests_PrintedById");

            migrationBuilder.AlterColumn<int>(
                name: "BookTenderId",
                table: "PendingRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BidBondId",
                table: "PendingRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "PendingRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PendingRequests",
                table: "PendingRequests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PendingRequests_BidBondId",
                table: "PendingRequests",
                column: "BidBondId",
                unique: true,
                filter: "[BidBondId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PendingRequests_BookTenderId",
                table: "PendingRequests",
                column: "BookTenderId",
                unique: true,
                filter: "[BookTenderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingRequests_AspNetUsers_PrintedById",
                table: "PendingRequests",
                column: "PrintedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingRequests_AspNetUsers_RequestedById",
                table: "PendingRequests",
                column: "RequestedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingRequests_AspNetUsers_SentById",
                table: "PendingRequests",
                column: "SentById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingRequests_AspNetUsers_StatusById",
                table: "PendingRequests",
                column: "StatusById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingRequests_BidBonds_BidBondId",
                table: "PendingRequests",
                column: "BidBondId",
                principalTable: "BidBonds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingRequests_BookTenders_BookTenderId",
                table: "PendingRequests",
                column: "BookTenderId",
                principalTable: "BookTenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
