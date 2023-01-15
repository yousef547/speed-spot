using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Quotations_LetterOfGuarantees_SplitTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequests_LetterOfGuarantees_FinalLGId",
                table: "FinalLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LetterOfGuarantees_AspNetUsers_AssignedToId",
                table: "LetterOfGuarantees");

            migrationBuilder.DropForeignKey(
                name: "FK_LetterOfGuarantees_BankBranches_BankBranchId",
                table: "LetterOfGuarantees");

            migrationBuilder.DropForeignKey(
                name: "FK_LetterOfGuarantees_Banks_BankId",
                table: "LetterOfGuarantees");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequests_LetterOfGuarantees_PerformanceLGId",
                table: "PerformanceLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_LetterOfGuarantees_FinalLGId",
                table: "Quotations");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_LetterOfGuarantees_PerformanceLGId",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_FinalLGId",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_PerformanceLGId",
                table: "Quotations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LetterOfGuarantees",
                table: "LetterOfGuarantees");

            migrationBuilder.DropColumn(
                name: "FinalLGId",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "PerformanceLGId",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "LetterOfGuarantees");

            migrationBuilder.RenameTable(
                name: "LetterOfGuarantees",
                newName: "PerformanceLGs");

            migrationBuilder.RenameIndex(
                name: "IX_LetterOfGuarantees_BankId",
                table: "PerformanceLGs",
                newName: "IX_PerformanceLGs_BankId");

            migrationBuilder.RenameIndex(
                name: "IX_LetterOfGuarantees_BankBranchId",
                table: "PerformanceLGs",
                newName: "IX_PerformanceLGs_BankBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_LetterOfGuarantees_AssignedToId",
                table: "PerformanceLGs",
                newName: "IX_PerformanceLGs_AssignedToId");

            migrationBuilder.AddColumn<int>(
                name: "QuotationId",
                table: "PerformanceLGs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerformanceLGs",
                table: "PerformanceLGs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FinalLGs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    AssignedToId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CashType = table.Column<bool>(type: "bit", nullable: false),
                    AttachmentTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCredit = table.Column<bool>(type: "bit", nullable: true),
                    NoOfPeriods = table.Column<int>(type: "int", nullable: true),
                    BankId = table.Column<int>(type: "int", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BankBranchId = table.Column<int>(type: "int", nullable: true),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Fees = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LetterOfGuaranteeNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalLGs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalLGs_AspNetUsers_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalLGs_BankBranches_BankBranchId",
                        column: x => x.BankBranchId,
                        principalTable: "BankBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalLGs_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalLGs_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceLGs_QuotationId",
                table: "PerformanceLGs",
                column: "QuotationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinalLGs_AssignedToId",
                table: "FinalLGs",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalLGs_BankBranchId",
                table: "FinalLGs",
                column: "BankBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalLGs_BankId",
                table: "FinalLGs",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalLGs_QuotationId",
                table: "FinalLGs",
                column: "QuotationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequests_FinalLGs_FinalLGId",
                table: "FinalLGRequests",
                column: "FinalLGId",
                principalTable: "FinalLGs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequests_PerformanceLGs_PerformanceLGId",
                table: "PerformanceLGRequests",
                column: "PerformanceLGId",
                principalTable: "PerformanceLGs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGs_AspNetUsers_AssignedToId",
                table: "PerformanceLGs",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGs_BankBranches_BankBranchId",
                table: "PerformanceLGs",
                column: "BankBranchId",
                principalTable: "BankBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGs_Banks_BankId",
                table: "PerformanceLGs",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGs_Quotations_QuotationId",
                table: "PerformanceLGs",
                column: "QuotationId",
                principalTable: "Quotations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequests_FinalLGs_FinalLGId",
                table: "FinalLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequests_PerformanceLGs_PerformanceLGId",
                table: "PerformanceLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGs_AspNetUsers_AssignedToId",
                table: "PerformanceLGs");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGs_BankBranches_BankBranchId",
                table: "PerformanceLGs");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGs_Banks_BankId",
                table: "PerformanceLGs");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGs_Quotations_QuotationId",
                table: "PerformanceLGs");

            migrationBuilder.DropTable(
                name: "FinalLGs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerformanceLGs",
                table: "PerformanceLGs");

            migrationBuilder.DropIndex(
                name: "IX_PerformanceLGs_QuotationId",
                table: "PerformanceLGs");

            migrationBuilder.DropColumn(
                name: "QuotationId",
                table: "PerformanceLGs");

            migrationBuilder.RenameTable(
                name: "PerformanceLGs",
                newName: "LetterOfGuarantees");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGs_BankId",
                table: "LetterOfGuarantees",
                newName: "IX_LetterOfGuarantees_BankId");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGs_BankBranchId",
                table: "LetterOfGuarantees",
                newName: "IX_LetterOfGuarantees_BankBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGs_AssignedToId",
                table: "LetterOfGuarantees",
                newName: "IX_LetterOfGuarantees_AssignedToId");

            migrationBuilder.AddColumn<int>(
                name: "FinalLGId",
                table: "Quotations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PerformanceLGId",
                table: "Quotations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "LetterOfGuarantees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LetterOfGuarantees",
                table: "LetterOfGuarantees",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_FinalLGId",
                table: "Quotations",
                column: "FinalLGId",
                unique: true,
                filter: "[FinalLGId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_PerformanceLGId",
                table: "Quotations",
                column: "PerformanceLGId",
                unique: true,
                filter: "[PerformanceLGId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequests_LetterOfGuarantees_FinalLGId",
                table: "FinalLGRequests",
                column: "FinalLGId",
                principalTable: "LetterOfGuarantees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LetterOfGuarantees_AspNetUsers_AssignedToId",
                table: "LetterOfGuarantees",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LetterOfGuarantees_BankBranches_BankBranchId",
                table: "LetterOfGuarantees",
                column: "BankBranchId",
                principalTable: "BankBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LetterOfGuarantees_Banks_BankId",
                table: "LetterOfGuarantees",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequests_LetterOfGuarantees_PerformanceLGId",
                table: "PerformanceLGRequests",
                column: "PerformanceLGId",
                principalTable: "LetterOfGuarantees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_LetterOfGuarantees_FinalLGId",
                table: "Quotations",
                column: "FinalLGId",
                principalTable: "LetterOfGuarantees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_LetterOfGuarantees_PerformanceLGId",
                table: "Quotations",
                column: "PerformanceLGId",
                principalTable: "LetterOfGuarantees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
