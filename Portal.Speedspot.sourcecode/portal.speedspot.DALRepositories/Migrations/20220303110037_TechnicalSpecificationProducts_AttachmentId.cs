using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class TechnicalSpecificationProducts_AttachmentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TechnicalSpecificationProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttachmentId",
                table: "TechnicalSpecificationProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalSpecificationProducts_AttachmentId",
                table: "TechnicalSpecificationProducts",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalSpecificationProducts_Attachments_AttachmentId",
                table: "TechnicalSpecificationProducts",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalSpecificationProducts_Attachments_AttachmentId",
                table: "TechnicalSpecificationProducts");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalSpecificationProducts_AttachmentId",
                table: "TechnicalSpecificationProducts");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "TechnicalSpecificationProducts");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TechnicalSpecificationProducts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
