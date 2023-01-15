using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Favourites_UniqueColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favourites_TypeId",
                table: "Favourites");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_TypeId_UserId_ItemId",
                table: "Favourites",
                columns: new[] { "TypeId", "UserId", "ItemId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favourites_TypeId_UserId_ItemId",
                table: "Favourites");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_TypeId",
                table: "Favourites",
                column: "TypeId");
        }
    }
}
