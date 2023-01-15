using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class OppotyunityTypes_EnumValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookTenders_Attachments_ReceiptAttachmentId",
                table: "BookTenders");

            migrationBuilder.AddColumn<int>(
                name: "EnumValue",
                table: "OpportunityTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ReceiptAttachmentId",
                table: "BookTenders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BookTenders_Attachments_ReceiptAttachmentId",
                table: "BookTenders",
                column: "ReceiptAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookTenders_Attachments_ReceiptAttachmentId",
                table: "BookTenders");

            migrationBuilder.DropColumn(
                name: "EnumValue",
                table: "OpportunityTypes");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiptAttachmentId",
                table: "BookTenders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTenders_Attachments_ReceiptAttachmentId",
                table: "BookTenders",
                column: "ReceiptAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
