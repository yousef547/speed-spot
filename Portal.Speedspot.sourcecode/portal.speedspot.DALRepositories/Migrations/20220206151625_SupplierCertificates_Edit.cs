using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class SupplierCertificates_Edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Attachments_AttachmentId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierCertificate_Certificate_CertificateId",
                table: "SupplierCertificate");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierCertificate_Suppliers_SupplierId",
                table: "SupplierCertificate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierCertificate",
                table: "SupplierCertificate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificate",
                table: "Certificate");

            migrationBuilder.RenameTable(
                name: "SupplierCertificate",
                newName: "SupplierCertificates");

            migrationBuilder.RenameTable(
                name: "Certificate",
                newName: "Certificates");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierCertificate_SupplierId",
                table: "SupplierCertificates",
                newName: "IX_SupplierCertificates_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierCertificate_CertificateId",
                table: "SupplierCertificates",
                newName: "IX_SupplierCertificates_CertificateId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificate_AttachmentId",
                table: "Certificates",
                newName: "IX_Certificates_AttachmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierCertificates",
                table: "SupplierCertificates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificates",
                table: "Certificates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Attachments_AttachmentId",
                table: "Certificates",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierCertificates_Certificates_CertificateId",
                table: "SupplierCertificates",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierCertificates_Suppliers_SupplierId",
                table: "SupplierCertificates",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Attachments_AttachmentId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierCertificates_Certificates_CertificateId",
                table: "SupplierCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierCertificates_Suppliers_SupplierId",
                table: "SupplierCertificates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierCertificates",
                table: "SupplierCertificates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificates",
                table: "Certificates");

            migrationBuilder.RenameTable(
                name: "SupplierCertificates",
                newName: "SupplierCertificate");

            migrationBuilder.RenameTable(
                name: "Certificates",
                newName: "Certificate");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierCertificates_SupplierId",
                table: "SupplierCertificate",
                newName: "IX_SupplierCertificate_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierCertificates_CertificateId",
                table: "SupplierCertificate",
                newName: "IX_SupplierCertificate_CertificateId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificates_AttachmentId",
                table: "Certificate",
                newName: "IX_Certificate_AttachmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierCertificate",
                table: "SupplierCertificate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificate",
                table: "Certificate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Attachments_AttachmentId",
                table: "Certificate",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierCertificate_Certificate_CertificateId",
                table: "SupplierCertificate",
                column: "CertificateId",
                principalTable: "Certificate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierCertificate_Suppliers_SupplierId",
                table: "SupplierCertificate",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
