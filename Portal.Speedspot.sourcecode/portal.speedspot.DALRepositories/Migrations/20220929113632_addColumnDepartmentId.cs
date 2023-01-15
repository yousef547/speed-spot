using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class addColumnDepartmentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Journals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Journals_DepartmentId",
                table: "Journals",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Departments_DepartmentId",
                table: "Journals",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Departments_DepartmentId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_DepartmentId",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Journals");
        }
    }
}
