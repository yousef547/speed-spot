using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class BidBonds_Workflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "BidBonds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "BidBonds",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequested",
                table: "BidBonds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "BidBonds",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SentById",
                table: "BidBonds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BidBonds_ApprovedById",
                table: "BidBonds",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_BidBonds_SentById",
                table: "BidBonds",
                column: "SentById");

            migrationBuilder.AddForeignKey(
                name: "FK_BidBonds_AspNetUsers_ApprovedById",
                table: "BidBonds",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BidBonds_AspNetUsers_SentById",
                table: "BidBonds",
                column: "SentById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidBonds_AspNetUsers_ApprovedById",
                table: "BidBonds");

            migrationBuilder.DropForeignKey(
                name: "FK_BidBonds_AspNetUsers_SentById",
                table: "BidBonds");

            migrationBuilder.DropIndex(
                name: "IX_BidBonds_ApprovedById",
                table: "BidBonds");

            migrationBuilder.DropIndex(
                name: "IX_BidBonds_SentById",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "IsRequested",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "SentById",
                table: "BidBonds");
        }
    }
}
