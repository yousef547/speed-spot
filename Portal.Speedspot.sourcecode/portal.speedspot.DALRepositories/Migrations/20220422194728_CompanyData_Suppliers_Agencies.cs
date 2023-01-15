using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class CompanyData_Suppliers_Agencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompetitorId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAgency",
                table: "Suppliers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CompanyData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CompetitorId",
                table: "Suppliers",
                column: "CompetitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Competitors_CompetitorId",
                table: "Suppliers",
                column: "CompetitorId",
                principalTable: "Competitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Competitors_CompetitorId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "CompanyData");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_CompetitorId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CompetitorId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IsAgency",
                table: "Suppliers");
        }
    }
}
