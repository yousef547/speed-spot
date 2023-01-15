using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Opportunity_DepartmentFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_DepartmentId",
                table: "Opportunities",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_Departments_DepartmentId",
                table: "Opportunities",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_Departments_DepartmentId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_DepartmentId",
                table: "Opportunities");
        }
    }
}
