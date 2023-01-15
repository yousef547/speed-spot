using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class ProductCategories_DepartmentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "ProductCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_DepartmentId",
                table: "ProductCategories",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Departments_DepartmentId",
                table: "ProductCategories",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Departments_DepartmentId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_DepartmentId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "ProductCategories");
        }
    }
}
