using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Logistics_AllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    LogoAttachmentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityTypeId = table.Column<int>(type: "int", nullable: true),
                    OrganizationTypeId = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    LegalInfoId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    BeneficiaryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeneficiaryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logistics_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Logistics_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Logistics_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Logistics_Attachments_LogoAttachmentId",
                        column: x => x.LogoAttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Logistics_LegalInfos_LegalInfoId",
                        column: x => x.LegalInfoId,
                        principalTable: "LegalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Logistics_Logistics_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Logistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Logistics_OrganizationTypes_OrganizationTypeId",
                        column: x => x.OrganizationTypeId,
                        principalTable: "OrganizationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogisticBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogisticId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticBanks_BankBranches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "BankBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogisticBanks_Logistics_LogisticId",
                        column: x => x.LogisticId,
                        principalTable: "Logistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogisticContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogisticId = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogisticContacts_Logistics_LogisticId",
                        column: x => x.LogisticId,
                        principalTable: "Logistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogisticDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogisticId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogisticDepartments_Logistics_LogisticId",
                        column: x => x.LogisticId,
                        principalTable: "Logistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogisticEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogisticId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticEmployees_Logistics_LogisticId",
                        column: x => x.LogisticId,
                        principalTable: "Logistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogisticEmployees_PartnerEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "PartnerEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogisticFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogisticId = table.Column<int>(type: "int", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticFiles_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogisticFiles_Logistics_LogisticId",
                        column: x => x.LogisticId,
                        principalTable: "Logistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogisticBankCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogisticBankId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticBankCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticBankCurrencies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogisticBankCurrencies_LogisticBanks_LogisticBankId",
                        column: x => x.LogisticBankId,
                        principalTable: "LogisticBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogisticBankCurrencies_CurrencyId",
                table: "LogisticBankCurrencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticBankCurrencies_LogisticBankId",
                table: "LogisticBankCurrencies",
                column: "LogisticBankId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticBanks_BranchId",
                table: "LogisticBanks",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticBanks_LogisticId",
                table: "LogisticBanks",
                column: "LogisticId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticContacts_ContactId",
                table: "LogisticContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticContacts_LogisticId",
                table: "LogisticContacts",
                column: "LogisticId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticDepartments_DepartmentId",
                table: "LogisticDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticDepartments_LogisticId",
                table: "LogisticDepartments",
                column: "LogisticId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticEmployees_EmployeeId",
                table: "LogisticEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticEmployees_LogisticId",
                table: "LogisticEmployees",
                column: "LogisticId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticFiles_AttachmentId",
                table: "LogisticFiles",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticFiles_LogisticId",
                table: "LogisticFiles",
                column: "LogisticId");

            migrationBuilder.CreateIndex(
                name: "IX_Logistics_ActivityTypeId",
                table: "Logistics",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Logistics_AddressId",
                table: "Logistics",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Logistics_CreatedById",
                table: "Logistics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Logistics_LegalInfoId",
                table: "Logistics",
                column: "LegalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Logistics_LogoAttachmentId",
                table: "Logistics",
                column: "LogoAttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Logistics_OrganizationTypeId",
                table: "Logistics",
                column: "OrganizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Logistics_ParentId",
                table: "Logistics",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogisticBankCurrencies");

            migrationBuilder.DropTable(
                name: "LogisticContacts");

            migrationBuilder.DropTable(
                name: "LogisticDepartments");

            migrationBuilder.DropTable(
                name: "LogisticEmployees");

            migrationBuilder.DropTable(
                name: "LogisticFiles");

            migrationBuilder.DropTable(
                name: "LogisticBanks");

            migrationBuilder.DropTable(
                name: "Logistics");
        }
    }
}
