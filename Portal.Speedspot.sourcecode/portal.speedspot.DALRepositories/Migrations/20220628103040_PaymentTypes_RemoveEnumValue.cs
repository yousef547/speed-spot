using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class PaymentTypes_RemoveEnumValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnumValue",
                table: "PaymentTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnumValue",
                table: "PaymentTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
