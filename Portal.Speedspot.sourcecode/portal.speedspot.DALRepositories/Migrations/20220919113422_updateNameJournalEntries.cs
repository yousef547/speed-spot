using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class updateNameJournalEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntry_FinancialAccounts_AccountId",
                table: "JournalEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntry_Journals_JournalId",
                table: "JournalEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JournalEntry",
                table: "JournalEntry");

            migrationBuilder.RenameTable(
                name: "JournalEntry",
                newName: "JournalEntries");

            migrationBuilder.RenameIndex(
                name: "IX_JournalEntry_JournalId",
                table: "JournalEntries",
                newName: "IX_JournalEntries_JournalId");

            migrationBuilder.RenameIndex(
                name: "IX_JournalEntry_AccountId",
                table: "JournalEntries",
                newName: "IX_JournalEntries_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JournalEntries",
                table: "JournalEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntries_FinancialAccounts_AccountId",
                table: "JournalEntries",
                column: "AccountId",
                principalTable: "FinancialAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntries_Journals_JournalId",
                table: "JournalEntries",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntries_FinancialAccounts_AccountId",
                table: "JournalEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntries_Journals_JournalId",
                table: "JournalEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JournalEntries",
                table: "JournalEntries");

            migrationBuilder.RenameTable(
                name: "JournalEntries",
                newName: "JournalEntry");

            migrationBuilder.RenameIndex(
                name: "IX_JournalEntries_JournalId",
                table: "JournalEntry",
                newName: "IX_JournalEntry_JournalId");

            migrationBuilder.RenameIndex(
                name: "IX_JournalEntries_AccountId",
                table: "JournalEntry",
                newName: "IX_JournalEntry_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JournalEntry",
                table: "JournalEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntry_FinancialAccounts_AccountId",
                table: "JournalEntry",
                column: "AccountId",
                principalTable: "FinancialAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntry_Journals_JournalId",
                table: "JournalEntry",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
