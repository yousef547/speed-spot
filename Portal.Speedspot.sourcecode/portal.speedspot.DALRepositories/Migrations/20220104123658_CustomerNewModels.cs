using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class CustomerNewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityTypeId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LegalInfoId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LogoAttachmentId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationTypeId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code2",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code3",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneCode",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ActivityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnumValue = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerFiles_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerFiles_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVendorRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BookRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(12,9)", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVendorRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerVendorRegistrations_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerVendorRegistrations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommercialRegister = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalInfos_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartnerEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneCodeId = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerEmployees_Countries_PhoneCodeId",
                        column: x => x.PhoneCodeId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<decimal>(type: "decimal(12,9)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(12,9)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    PhoneCodeId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_Countries_PhoneCodeId",
                        column: x => x.PhoneCodeId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerEmployees_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerEmployees_PartnerEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "PartnerEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ActivityTypeId",
                table: "Customers",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LegalInfoId",
                table: "Customers",
                column: "LegalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LogoAttachmentId",
                table: "Customers",
                column: "LogoAttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_OrganizationTypeId",
                table: "Customers",
                column: "OrganizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ParentId",
                table: "Customers",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PhoneCodeId",
                table: "Contacts",
                column: "PhoneCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TypeId",
                table: "Contacts",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_ContactId",
                table: "CustomerContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerId",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEmployees_CustomerId",
                table: "CustomerEmployees",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEmployees_EmployeeId",
                table: "CustomerEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFiles_AttachmentId",
                table: "CustomerFiles",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFiles_CustomerId",
                table: "CustomerFiles",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVendorRegistrations_AttachmentId",
                table: "CustomerVendorRegistrations",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVendorRegistrations_CustomerId",
                table: "CustomerVendorRegistrations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalInfos_AttachmentId",
                table: "LegalInfos",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerEmployees_PhoneCodeId",
                table: "PartnerEmployees",
                column: "PhoneCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_ActivityTypes_ActivityTypeId",
                table: "Customers",
                column: "ActivityTypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Attachments_LogoAttachmentId",
                table: "Customers",
                column: "LogoAttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Customers_ParentId",
                table: "Customers",
                column: "ParentId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_LegalInfos_LegalInfoId",
                table: "Customers",
                column: "LegalInfoId",
                principalTable: "LegalInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_OrganizationTypes_OrganizationTypeId",
                table: "Customers",
                column: "OrganizationTypeId",
                principalTable: "OrganizationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_ActivityTypes_ActivityTypeId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Attachments_LogoAttachmentId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Customers_ParentId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_LegalInfos_LegalInfoId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_OrganizationTypes_OrganizationTypeId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CustomerContacts");

            migrationBuilder.DropTable(
                name: "CustomerEmployees");

            migrationBuilder.DropTable(
                name: "CustomerFiles");

            migrationBuilder.DropTable(
                name: "CustomerVendorRegistrations");

            migrationBuilder.DropTable(
                name: "LegalInfos");

            migrationBuilder.DropTable(
                name: "OrganizationTypes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "PartnerEmployees");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ActivityTypeId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_AddressId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_LegalInfoId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_LogoAttachmentId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_OrganizationTypeId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ParentId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ActivityTypeId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LegalInfoId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LogoAttachmentId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "OrganizationTypeId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SubName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Code2",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Code3",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "PhoneCode",
                table: "Countries");
        }
    }
}
