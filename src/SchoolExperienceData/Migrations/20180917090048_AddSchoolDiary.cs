using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolExperienceData.Migrations
{
    public partial class AddSchoolDiary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Candidates",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SchoolDiary",
                columns: table => new
                {
                    CandidateDiaryId = table.Column<int>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchoolId = table.Column<string>(nullable: true),
                    When = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolDiary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolDiary_CandidateDiarys_CandidateDiaryId",
                        column: x => x.CandidateDiaryId,
                        principalTable: "CandidateDiarys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolDiary_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolDiary_CandidateDiaryId",
                table: "SchoolDiary",
                column: "CandidateDiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolDiary_SchoolId",
                table: "SchoolDiary",
                column: "SchoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolDiary");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Candidates");
        }
    }
}
