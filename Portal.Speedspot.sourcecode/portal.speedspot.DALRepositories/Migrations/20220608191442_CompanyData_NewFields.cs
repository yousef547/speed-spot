using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class CompanyData_NewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "CompanyData");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "CompanyData");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "CompanyData");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "CompanyData");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "CompanyData");
        }
    }
}
