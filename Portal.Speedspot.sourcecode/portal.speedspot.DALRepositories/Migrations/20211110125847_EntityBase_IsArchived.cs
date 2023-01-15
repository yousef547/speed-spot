using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class EntityBase_IsArchived : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "OpportunityTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "OpportunityStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Opportunities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "EmployeeTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Countries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "BookTenders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "BidBonds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Banks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Attachments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "OpportunityTypes");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "OpportunityStatuses");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "EmployeeTypes");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "BookTenders");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "BidBonds");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Attachments");
        }
    }
}
