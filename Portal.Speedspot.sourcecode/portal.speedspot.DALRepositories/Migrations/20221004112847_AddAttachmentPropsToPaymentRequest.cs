using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class AddAttachmentPropsToPaymentRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CashAttachmentId",
                table: "PaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiptAttachmentId",
                table: "PaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransferAttachmentId",
                table: "PaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_CashAttachmentId",
                table: "PaymentRequests",
                column: "CashAttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_ReceiptAttachmentId",
                table: "PaymentRequests",
                column: "ReceiptAttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_TransferAttachmentId",
                table: "PaymentRequests",
                column: "TransferAttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_Attachments_CashAttachmentId",
                table: "PaymentRequests",
                column: "CashAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_Attachments_ReceiptAttachmentId",
                table: "PaymentRequests",
                column: "ReceiptAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_Attachments_TransferAttachmentId",
                table: "PaymentRequests",
                column: "TransferAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_Attachments_CashAttachmentId",
                table: "PaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_Attachments_ReceiptAttachmentId",
                table: "PaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_Attachments_TransferAttachmentId",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_CashAttachmentId",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_ReceiptAttachmentId",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_TransferAttachmentId",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "CashAttachmentId",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "ReceiptAttachmentId",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "TransferAttachmentId",
                table: "PaymentRequests");
        }
    }
}
