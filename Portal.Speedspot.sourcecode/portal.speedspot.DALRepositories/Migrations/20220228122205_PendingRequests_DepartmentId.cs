using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class PendingRequests_DepartmentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "BookTenderRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "BidBondRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookTenderRequests_DepartmentId",
                table: "BookTenderRequests",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BidBondRequests_DepartmentId",
                table: "BidBondRequests",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BidBondRequests_Departments_DepartmentId",
                table: "BidBondRequests",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTenderRequests_Departments_DepartmentId",
                table: "BookTenderRequests",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidBondRequests_Departments_DepartmentId",
                table: "BidBondRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTenderRequests_Departments_DepartmentId",
                table: "BookTenderRequests");

            migrationBuilder.DropIndex(
                name: "IX_BookTenderRequests_DepartmentId",
                table: "BookTenderRequests");

            migrationBuilder.DropIndex(
                name: "IX_BidBondRequests_DepartmentId",
                table: "BidBondRequests");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "BookTenderRequests");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "BidBondRequests");
        }
    }
}
