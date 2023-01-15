using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class Opportunities_TechnicalSpecifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpportunityId = table.Column<int>(type: "int", nullable: false),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalSpecifications_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalSpecificationAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnicalSpecificationId = table.Column<int>(type: "int", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalSpecificationAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalSpecificationAttachments_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicalSpecificationAttachments_TechnicalSpecifications_TechnicalSpecificationId",
                        column: x => x.TechnicalSpecificationId,
                        principalTable: "TechnicalSpecifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalSpecificationProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnicalSpecificationId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalSpecificationProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalSpecificationProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicalSpecificationProducts_TechnicalSpecifications_TechnicalSpecificationId",
                        column: x => x.TechnicalSpecificationId,
                        principalTable: "TechnicalSpecifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalSpecificationAttachments_AttachmentId",
                table: "TechnicalSpecificationAttachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalSpecificationAttachments_TechnicalSpecificationId",
                table: "TechnicalSpecificationAttachments",
                column: "TechnicalSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalSpecificationProducts_ProductId",
                table: "TechnicalSpecificationProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalSpecificationProducts_TechnicalSpecificationId",
                table: "TechnicalSpecificationProducts",
                column: "TechnicalSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalSpecifications_OpportunityId",
                table: "TechnicalSpecifications",
                column: "OpportunityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicalSpecificationAttachments");

            migrationBuilder.DropTable(
                name: "TechnicalSpecificationProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TechnicalSpecifications");
        }
    }
}
