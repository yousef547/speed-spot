using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Suppliers_New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityTypeId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LogoAttachmentId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationTypeId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubName",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SupplierContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierContacts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierDepartments_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierEmployees_PartnerEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "PartnerEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierEmployees_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierFiles_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierFiles_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ActivityTypeId",
                table: "Suppliers",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_AddressId",
                table: "Suppliers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_LogoAttachmentId",
                table: "Suppliers",
                column: "LogoAttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_OrganizationTypeId",
                table: "Suppliers",
                column: "OrganizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ParentId",
                table: "Suppliers",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContacts_ContactId",
                table: "SupplierContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContacts_SupplierId",
                table: "SupplierContacts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierDepartments_DepartmentId",
                table: "SupplierDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierDepartments_SupplierId",
                table: "SupplierDepartments",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierEmployees_EmployeeId",
                table: "SupplierEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierEmployees_SupplierId",
                table: "SupplierEmployees",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierFiles_AttachmentId",
                table: "SupplierFiles",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierFiles_SupplierId",
                table: "SupplierFiles",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_ActivityTypes_ActivityTypeId",
                table: "Suppliers",
                column: "ActivityTypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Addresses_AddressId",
                table: "Suppliers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Attachments_LogoAttachmentId",
                table: "Suppliers",
                column: "LogoAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_OrganizationTypes_OrganizationTypeId",
                table: "Suppliers",
                column: "OrganizationTypeId",
                principalTable: "OrganizationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Suppliers_ParentId",
                table: "Suppliers",
                column: "ParentId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_ActivityTypes_ActivityTypeId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Addresses_AddressId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Attachments_LogoAttachmentId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_OrganizationTypes_OrganizationTypeId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Suppliers_ParentId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "SupplierContacts");

            migrationBuilder.DropTable(
                name: "SupplierDepartments");

            migrationBuilder.DropTable(
                name: "SupplierEmployees");

            migrationBuilder.DropTable(
                name: "SupplierFiles");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_ActivityTypeId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_AddressId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_LogoAttachmentId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_OrganizationTypeId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_ParentId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ActivityTypeId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "LogoAttachmentId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "OrganizationTypeId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "SubName",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "Suppliers");
        }
    }
}
