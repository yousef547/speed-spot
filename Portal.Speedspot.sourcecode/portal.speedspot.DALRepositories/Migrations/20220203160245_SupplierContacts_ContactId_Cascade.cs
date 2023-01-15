using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class SupplierContacts_ContactId_Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_SupplierContacts_Contacts_ContactId",
               table: "SupplierContacts");

            migrationBuilder.DropIndex(
               name: "IX_SupplierContacts_ContactId",
               table: "SupplierContacts");

            migrationBuilder.CreateIndex(
               name: "IX_SupplierContacts_ContactId",
               table: "SupplierContacts",
               column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierContacts_Contacts_ContactId",
                table: "SupplierContacts",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierContacts_Contacts_ContactId",
                table: "SupplierContacts");

            migrationBuilder.DropIndex(
                name: "IX_SupplierContacts_ContactId",
                table: "SupplierContacts");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContacts_ContactId",
                table: "SupplierContacts",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierContacts_Contacts_ContactId",
                table: "SupplierContacts",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
