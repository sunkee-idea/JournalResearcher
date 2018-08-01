using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalResearcher.DataAccess.Migrations
{
    public partial class JournalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Journals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Abstract = table.Column<string>(maxLength: 255, nullable: false),
                    Author = table.Column<string>(maxLength: 255, nullable: false),
                    Reference = table.Column<string>(maxLength: 255, nullable: false),
                    SupervisorName = table.Column<string>(maxLength: 255, nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    ThesisDateTime = table.Column<DateTime>(nullable: false),
                    ThesisFile = table.Column<string>(nullable: false),
                    DateSubmitted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Journals");
        }
    }
}
