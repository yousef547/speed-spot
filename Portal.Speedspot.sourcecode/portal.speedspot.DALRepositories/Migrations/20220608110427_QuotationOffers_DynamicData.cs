using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class QuotationOffers_DynamicData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultOfferCoverLetter",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagingDirectorId",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesDirectorId",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionAr",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DefaultOfferNotes",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagingDirectorId",
                table: "Departments",
                column: "ManagingDirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_SalesDirectorId",
                table: "Departments",
                column: "SalesDirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_ManagingDirectorId",
                table: "Departments",
                column: "ManagingDirectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_SalesDirectorId",
                table: "Departments",
                column: "SalesDirectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_ManagingDirectorId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_SalesDirectorId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ManagingDirectorId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_SalesDirectorId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DefaultOfferCoverLetter",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ManagingDirectorId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "SalesDirectorId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "CompanyData");

            migrationBuilder.DropColumn(
                name: "DefaultOfferNotes",
                table: "CompanyData");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionAr",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CompanyData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
