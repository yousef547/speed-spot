using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class SupplierOfferProducts_AttachmentId_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOfferProducts_Attachments_AttachmentId",
                table: "SupplierOfferProducts");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                table: "SupplierOfferProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOfferProducts_Attachments_AttachmentId",
                table: "SupplierOfferProducts",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOfferProducts_Attachments_AttachmentId",
                table: "SupplierOfferProducts");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                table: "SupplierOfferProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOfferProducts_Attachments_AttachmentId",
                table: "SupplierOfferProducts",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
