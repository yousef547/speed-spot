using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class RenameProjectDateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndData",
                table: "Projects",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "CreatedData",
                table: "Projects",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Projects",
                newName: "EndData");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Projects",
                newName: "CreatedData");
        }
    }
}
