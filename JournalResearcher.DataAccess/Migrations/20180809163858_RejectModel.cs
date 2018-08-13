using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalResearcher.DataAccess.Migrations
{
    public partial class RejectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RejectJournals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RejectedJournalId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    JournalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejectJournals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RejectJournals_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RejectJournals_Journals_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RejectJournals_ApplicationUserId",
                table: "RejectJournals",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectJournals_JournalId",
                table: "RejectJournals",
                column: "JournalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RejectJournals");
        }
    }
}
