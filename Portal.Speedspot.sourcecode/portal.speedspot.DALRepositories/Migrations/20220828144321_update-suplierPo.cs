using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class updatesuplierPo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliverPeriod",
                table: "SupplierPos",
                newName: "DeliveryPeriod");

            migrationBuilder.RenameColumn(
                name: "CustomerPONumber",
                table: "SupplierPos",
                newName: "SupplierPONumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierPONumber",
                table: "SupplierPos",
                newName: "CustomerPONumber");

            migrationBuilder.RenameColumn(
                name: "DeliveryPeriod",
                table: "SupplierPos",
                newName: "DeliverPeriod");
        }
    }
}
