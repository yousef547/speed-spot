using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class SupplierEmployees_EmployeeId_Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
              name: "FK_SupplierEmployees_PartnerEmployees_EmployeeId",
              table: "SupplierEmployees");

            migrationBuilder.DropIndex(
               name: "IX_SupplierEmployees_EmployeeId",
               table: "SupplierEmployees");

            migrationBuilder.CreateIndex(
               name: "IX_SupplierEmployees_EmployeeId",
               table: "SupplierEmployees",
               column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierEmployees_PartnerEmployees_EmployeeId",
                table: "SupplierEmployees",
                column: "EmployeeId",
                principalTable: "PartnerEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
              name: "FK_SupplierEmployees_PartnerEmployees_EmployeeId",
              table: "SupplierEmployees");

            migrationBuilder.DropIndex(
              name: "IX_SupplierEmployees_EmployeeId",
              table: "SupplierEmployees");

            migrationBuilder.CreateIndex(
               name: "IX_SupplierEmployees_EmployeeId",
               table: "SupplierEmployees",
               column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierEmployees_PartnerEmployees_EmployeeId",
                table: "SupplierEmployees",
                column: "EmployeeId",
                principalTable: "PartnerEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
