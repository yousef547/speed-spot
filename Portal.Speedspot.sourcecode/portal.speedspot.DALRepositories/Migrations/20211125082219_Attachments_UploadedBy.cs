using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Attachments_UploadedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UploadedById",
                table: "Attachments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_UploadedById",
                table: "Attachments",
                column: "UploadedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_AspNetUsers_UploadedById",
                table: "Attachments",
                column: "UploadedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_AspNetUsers_UploadedById",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_UploadedById",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "UploadedById",
                table: "Attachments");
        }
    }
}
