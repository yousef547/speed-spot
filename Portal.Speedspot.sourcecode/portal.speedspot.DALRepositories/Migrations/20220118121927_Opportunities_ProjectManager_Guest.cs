using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Opportunities_ProjectManager_Guest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuestId",
                table: "Opportunities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectManagerId",
                table: "Opportunities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_GuestId",
                table: "Opportunities",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_ProjectManagerId",
                table: "Opportunities",
                column: "ProjectManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_AspNetUsers_GuestId",
                table: "Opportunities",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_AspNetUsers_ProjectManagerId",
                table: "Opportunities",
                column: "ProjectManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_AspNetUsers_GuestId",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_AspNetUsers_ProjectManagerId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_GuestId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_ProjectManagerId",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "Opportunities");
        }
    }
}
