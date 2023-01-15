using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class UpdateTableStickyNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PageTitle",
                table: "StickyNotes",
                newName: "PageController");

            migrationBuilder.AddColumn<string>(
                name: "PageAction",
                table: "StickyNotes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageAction",
                table: "StickyNotes");

            migrationBuilder.RenameColumn(
                name: "PageController",
                table: "StickyNotes",
                newName: "PageTitle");
        }
    }
}
