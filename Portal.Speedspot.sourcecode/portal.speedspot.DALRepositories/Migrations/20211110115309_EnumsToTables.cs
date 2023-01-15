using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class EnumsToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Opportunities",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Opportunities",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "EmployeeType",
                table: "AspNetUsers",
                newName: "EmployeeTypeId");

            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_StatusId",
                table: "Opportunities",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_TypeId",
                table: "Opportunities",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeeTypeId",
                table: "AspNetUsers",
                column: "EmployeeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EmployeeTypes_EmployeeTypeId",
                table: "AspNetUsers",
                column: "EmployeeTypeId",
                principalTable: "EmployeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_OpportunityStatuses_StatusId",
                table: "Opportunities",
                column: "StatusId",
                principalTable: "OpportunityStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_OpportunityTypes_TypeId",
                table: "Opportunities",
                column: "TypeId",
                principalTable: "OpportunityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EmployeeTypes_EmployeeTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_OpportunityStatuses_StatusId",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_OpportunityTypes_TypeId",
                table: "Opportunities");

            migrationBuilder.DropTable(
                name: "EmployeeTypes");

            migrationBuilder.DropTable(
                name: "OpportunityStatuses");

            migrationBuilder.DropTable(
                name: "OpportunityTypes");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_StatusId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_TypeId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmployeeTypeId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Opportunities",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Opportunities",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "EmployeeTypeId",
                table: "AspNetUsers",
                newName: "EmployeeType");
        }
    }
}
