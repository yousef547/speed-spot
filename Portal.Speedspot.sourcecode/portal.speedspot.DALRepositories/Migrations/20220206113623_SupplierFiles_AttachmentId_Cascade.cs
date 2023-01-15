using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class SupplierFiles_AttachmentId_Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
             name: "FK_SupplierFiles_Attachments_AttachmentId",
             table: "SupplierFiles");

            migrationBuilder.DropIndex(
               name: "IX_SupplierFiles_AttachmentId",
               table: "SupplierFiles");

            migrationBuilder.CreateIndex(
               name: "IX_SupplierFiles_AttachmentId",
               table: "SupplierFiles",
               column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierFiles_Attachments_AttachmentId",
                table: "SupplierFiles",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
             name: "FK_SupplierFiles_Attachments_AttachmentId",
             table: "SupplierFiles");

            migrationBuilder.DropIndex(
              name: "IX_SupplierFiles_AttachmentId",
              table: "SupplierFiles");

            migrationBuilder.CreateIndex(
               name: "IX_SupplierFiles_AttachmentId",
               table: "SupplierFiles",
               column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierFiles_Attachments_AttachmentId",
                table: "SupplierFiles",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
