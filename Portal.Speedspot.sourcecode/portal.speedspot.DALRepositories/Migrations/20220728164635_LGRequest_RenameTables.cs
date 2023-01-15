using Microsoft.EntityFrameworkCore.Migrations;

namespace portal.speedspot.DALRepositories.Migrations
{
    public partial class LGRequest_RenameTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequest_AspNetUsers_PrintedById",
                table: "FinalLGRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequest_AspNetUsers_RequestedById",
                table: "FinalLGRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequest_AspNetUsers_SentById",
                table: "FinalLGRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequest_AspNetUsers_StatusById",
                table: "FinalLGRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequest_Departments_DepartmentId",
                table: "FinalLGRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequest_LetterOfGuarantees_FinalLGId",
                table: "FinalLGRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequest_AspNetUsers_PrintedById",
                table: "PerformanceLGRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequest_AspNetUsers_RequestedById",
                table: "PerformanceLGRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequest_AspNetUsers_SentById",
                table: "PerformanceLGRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequest_AspNetUsers_StatusById",
                table: "PerformanceLGRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequest_Departments_DepartmentId",
                table: "PerformanceLGRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequest_LetterOfGuarantees_PerformanceLGId",
                table: "PerformanceLGRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerformanceLGRequest",
                table: "PerformanceLGRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinalLGRequest",
                table: "FinalLGRequest");

            migrationBuilder.RenameTable(
                name: "PerformanceLGRequest",
                newName: "PerformanceLGRequests");

            migrationBuilder.RenameTable(
                name: "FinalLGRequest",
                newName: "FinalLGRequests");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGRequest_StatusById",
                table: "PerformanceLGRequests",
                newName: "IX_PerformanceLGRequests_StatusById");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGRequest_SentById",
                table: "PerformanceLGRequests",
                newName: "IX_PerformanceLGRequests_SentById");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGRequest_RequestedById",
                table: "PerformanceLGRequests",
                newName: "IX_PerformanceLGRequests_RequestedById");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGRequest_PrintedById",
                table: "PerformanceLGRequests",
                newName: "IX_PerformanceLGRequests_PrintedById");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGRequest_PerformanceLGId",
                table: "PerformanceLGRequests",
                newName: "IX_PerformanceLGRequests_PerformanceLGId");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGRequest_DepartmentId",
                table: "PerformanceLGRequests",
                newName: "IX_PerformanceLGRequests_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_FinalLGRequest_StatusById",
                table: "FinalLGRequests",
                newName: "IX_FinalLGRequests_StatusById");

            migrationBuilder.RenameIndex(
                name: "IX_FinalLGRequest_SentById",
                table: "FinalLGRequests",
                newName: "IX_FinalLGRequests_SentById");

            migrationBuilder.RenameIndex(
                name: "IX_FinalLGRequest_RequestedById",
                table: "FinalLGRequests",
                newName: "IX_FinalLGRequests_RequestedById");

            migrationBuilder.RenameIndex(
                name: "IX_FinalLGRequest_PrintedById",
                table: "FinalLGRequests",
                newName: "IX_FinalLGRequests_PrintedById");

            migrationBuilder.RenameIndex(
                name: "IX_FinalLGRequest_FinalLGId",
                table: "FinalLGRequests",
                newName: "IX_FinalLGRequests_FinalLGId");

            migrationBuilder.RenameIndex(
                name: "IX_FinalLGRequest_DepartmentId",
                table: "FinalLGRequests",
                newName: "IX_FinalLGRequests_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerformanceLGRequests",
                table: "PerformanceLGRequests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinalLGRequests",
                table: "FinalLGRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequests_AspNetUsers_PrintedById",
                table: "FinalLGRequests",
                column: "PrintedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequests_AspNetUsers_RequestedById",
                table: "FinalLGRequests",
                column: "RequestedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequests_AspNetUsers_SentById",
                table: "FinalLGRequests",
                column: "SentById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequests_AspNetUsers_StatusById",
                table: "FinalLGRequests",
                column: "StatusById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequests_Departments_DepartmentId",
                table: "FinalLGRequests",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequests_LetterOfGuarantees_FinalLGId",
                table: "FinalLGRequests",
                column: "FinalLGId",
                principalTable: "LetterOfGuarantees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequests_AspNetUsers_PrintedById",
                table: "PerformanceLGRequests",
                column: "PrintedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequests_AspNetUsers_RequestedById",
                table: "PerformanceLGRequests",
                column: "RequestedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequests_AspNetUsers_SentById",
                table: "PerformanceLGRequests",
                column: "SentById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequests_AspNetUsers_StatusById",
                table: "PerformanceLGRequests",
                column: "StatusById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequests_Departments_DepartmentId",
                table: "PerformanceLGRequests",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequests_LetterOfGuarantees_PerformanceLGId",
                table: "PerformanceLGRequests",
                column: "PerformanceLGId",
                principalTable: "LetterOfGuarantees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequests_AspNetUsers_PrintedById",
                table: "FinalLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequests_AspNetUsers_RequestedById",
                table: "FinalLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequests_AspNetUsers_SentById",
                table: "FinalLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequests_AspNetUsers_StatusById",
                table: "FinalLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequests_Departments_DepartmentId",
                table: "FinalLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalLGRequests_LetterOfGuarantees_FinalLGId",
                table: "FinalLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequests_AspNetUsers_PrintedById",
                table: "PerformanceLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequests_AspNetUsers_RequestedById",
                table: "PerformanceLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequests_AspNetUsers_SentById",
                table: "PerformanceLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequests_AspNetUsers_StatusById",
                table: "PerformanceLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequests_Departments_DepartmentId",
                table: "PerformanceLGRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceLGRequests_LetterOfGuarantees_PerformanceLGId",
                table: "PerformanceLGRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerformanceLGRequests",
                table: "PerformanceLGRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinalLGRequests",
                table: "FinalLGRequests");

            migrationBuilder.RenameTable(
                name: "PerformanceLGRequests",
                newName: "PerformanceLGRequest");

            migrationBuilder.RenameTable(
                name: "FinalLGRequests",
                newName: "FinalLGRequest");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGRequests_StatusById",
                table: "PerformanceLGRequest",
                newName: "IX_PerformanceLGRequest_StatusById");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGRequests_SentById",
                table: "PerformanceLGRequest",
                newName: "IX_PerformanceLGRequest_SentById");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGRequests_RequestedById",
                table: "PerformanceLGRequest",
                newName: "IX_PerformanceLGRequest_RequestedById");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGRequests_PrintedById",
                table: "PerformanceLGRequest",
                newName: "IX_PerformanceLGRequest_PrintedById");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGRequests_PerformanceLGId",
                table: "PerformanceLGRequest",
                newName: "IX_PerformanceLGRequest_PerformanceLGId");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceLGRequests_DepartmentId",
                table: "PerformanceLGRequest",
                newName: "IX_PerformanceLGRequest_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_FinalLGRequests_StatusById",
                table: "FinalLGRequest",
                newName: "IX_FinalLGRequest_StatusById");

            migrationBuilder.RenameIndex(
                name: "IX_FinalLGRequests_SentById",
                table: "FinalLGRequest",
                newName: "IX_FinalLGRequest_SentById");

            migrationBuilder.RenameIndex(
                name: "IX_FinalLGRequests_RequestedById",
                table: "FinalLGRequest",
                newName: "IX_FinalLGRequest_RequestedById");

            migrationBuilder.RenameIndex(
                name: "IX_FinalLGRequests_PrintedById",
                table: "FinalLGRequest",
                newName: "IX_FinalLGRequest_PrintedById");

            migrationBuilder.RenameIndex(
                name: "IX_FinalLGRequests_FinalLGId",
                table: "FinalLGRequest",
                newName: "IX_FinalLGRequest_FinalLGId");

            migrationBuilder.RenameIndex(
                name: "IX_FinalLGRequests_DepartmentId",
                table: "FinalLGRequest",
                newName: "IX_FinalLGRequest_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerformanceLGRequest",
                table: "PerformanceLGRequest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinalLGRequest",
                table: "FinalLGRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequest_AspNetUsers_PrintedById",
                table: "FinalLGRequest",
                column: "PrintedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequest_AspNetUsers_RequestedById",
                table: "FinalLGRequest",
                column: "RequestedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequest_AspNetUsers_SentById",
                table: "FinalLGRequest",
                column: "SentById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequest_AspNetUsers_StatusById",
                table: "FinalLGRequest",
                column: "StatusById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequest_Departments_DepartmentId",
                table: "FinalLGRequest",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalLGRequest_LetterOfGuarantees_FinalLGId",
                table: "FinalLGRequest",
                column: "FinalLGId",
                principalTable: "LetterOfGuarantees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequest_AspNetUsers_PrintedById",
                table: "PerformanceLGRequest",
                column: "PrintedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequest_AspNetUsers_RequestedById",
                table: "PerformanceLGRequest",
                column: "RequestedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequest_AspNetUsers_SentById",
                table: "PerformanceLGRequest",
                column: "SentById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequest_AspNetUsers_StatusById",
                table: "PerformanceLGRequest",
                column: "StatusById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequest_Departments_DepartmentId",
                table: "PerformanceLGRequest",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceLGRequest_LetterOfGuarantees_PerformanceLGId",
                table: "PerformanceLGRequest",
                column: "PerformanceLGId",
                principalTable: "LetterOfGuarantees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
