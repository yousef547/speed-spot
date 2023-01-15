using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class updatetableprject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Quotations_QuotationId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "QuotationId",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Quotations_QuotationId",
                table: "Projects",
                column: "QuotationId",
                principalTable: "Quotations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Quotations_QuotationId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "QuotationId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Quotations_QuotationId",
                table: "Projects",
                column: "QuotationId",
                principalTable: "Quotations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
