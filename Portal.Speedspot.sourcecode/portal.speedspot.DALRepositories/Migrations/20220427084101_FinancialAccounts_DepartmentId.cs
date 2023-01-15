using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class FinancialAccounts_DepartmentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "FinancialAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAccounts_DepartmentId",
                table: "FinancialAccounts",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAccounts_Departments_DepartmentId",
                table: "FinancialAccounts",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAccounts_Departments_DepartmentId",
                table: "FinancialAccounts");

            migrationBuilder.DropIndex(
                name: "IX_FinancialAccounts_DepartmentId",
                table: "FinancialAccounts");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "FinancialAccounts");
        }
    }
}
