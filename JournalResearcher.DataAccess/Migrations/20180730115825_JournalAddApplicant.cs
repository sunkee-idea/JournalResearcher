using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalResearcher.DataAccess.Migrations
{
    public partial class JournalAddApplicant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicantId",
                table: "Journals",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_ApplicantId",
                table: "Journals",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_AspNetUsers_ApplicantId",
                table: "Journals",
                column: "ApplicantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_AspNetUsers_ApplicantId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_ApplicantId",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Journals");
        }
    }
}
