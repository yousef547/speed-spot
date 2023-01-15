using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Competitors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    LogoAttachmentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityTypeId = table.Column<int>(type: "int", nullable: true),
                    OrganizationTypeId = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    LegalInfoId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competitors_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competitors_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competitors_Attachments_LogoAttachmentId",
                        column: x => x.LogoAttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competitors_Competitors_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Competitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competitors_LegalInfos_LegalInfoId",
                        column: x => x.LegalInfoId,
                        principalTable: "LegalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competitors_OrganizationTypes_OrganizationTypeId",
                        column: x => x.OrganizationTypeId,
                        principalTable: "OrganizationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetitorContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitorId = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitorContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitorContacts_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalTable: "Competitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitorContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitorEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitorId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitorEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitorEmployees_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalTable: "Competitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitorEmployees_PartnerEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "PartnerEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitorFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitorId = table.Column<int>(type: "int", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitorFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitorFiles_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitorFiles_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalTable: "Competitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitorProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitorProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitorProducts_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalTable: "Competitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitorProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorContacts_CompetitorId",
                table: "CompetitorContacts",
                column: "CompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorContacts_ContactId",
                table: "CompetitorContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorEmployees_CompetitorId",
                table: "CompetitorEmployees",
                column: "CompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorEmployees_EmployeeId",
                table: "CompetitorEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorFiles_AttachmentId",
                table: "CompetitorFiles",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorFiles_CompetitorId",
                table: "CompetitorFiles",
                column: "CompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorProducts_CompetitorId",
                table: "CompetitorProducts",
                column: "CompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorProducts_ProductId",
                table: "CompetitorProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_ActivityTypeId",
                table: "Competitors",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_AddressId",
                table: "Competitors",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_LegalInfoId",
                table: "Competitors",
                column: "LegalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_LogoAttachmentId",
                table: "Competitors",
                column: "LogoAttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_OrganizationTypeId",
                table: "Competitors",
                column: "OrganizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_ParentId",
                table: "Competitors",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitorContacts");

            migrationBuilder.DropTable(
                name: "CompetitorEmployees");

            migrationBuilder.DropTable(
                name: "CompetitorFiles");

            migrationBuilder.DropTable(
                name: "CompetitorProducts");

            migrationBuilder.DropTable(
                name: "Competitors");
        }
    }
}
