using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolExperienceData.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GitisUsers",
                columns: table => new
                {
                    Address = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GitisUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailAddress = table.Column<string>(nullable: true),
                    Sent = table.Column<DateTime>(nullable: false),
                    TemplateId = table.Column<string>(nullable: true),
                    SendGroupReference = table.Column<string>(nullable: true),
                    NotificationId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    PlacementFee = table.Column<int>(nullable: false),
                    DbsRequirement = table.Column<int>(nullable: false),
                    DressCodeDetails = table.Column<string>(nullable: true),
                    CandidateParkingDetails = table.Column<string>(nullable: true),
                    PlacementStartTime = table.Column<TimeSpan>(nullable: false),
                    PlacementFinishTime = table.Column<TimeSpan>(nullable: false),
                    URN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolUsers",
                columns: table => new
                {
                    DfeReference = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolCalendar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Start = table.Column<DateTime>(nullable: false),
                    Finish = table.Column<DateTime>(nullable: false),
                    SchoolId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolCalendar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolCalendar_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SchoolId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolSchoolUserJoin",
                columns: table => new
                {
                    SchoolId = table.Column<int>(nullable: false),
                    SchoolUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSchoolUserJoin", x => new { x.SchoolId, x.SchoolUserId });
                    table.ForeignKey(
                        name: "FK_SchoolSchoolUserJoin_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolSchoolUserJoin_SchoolUsers_SchoolUserId",
                        column: x => x.SchoolUserId,
                        principalTable: "SchoolUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessibilityRequirements = table.Column<string>(nullable: true),
                    Degree = table.Column<int>(nullable: false),
                    DegreeOther = table.Column<string>(nullable: true),
                    DegreeSubjectId = table.Column<int>(nullable: true),
                    ExpectationsQuestion = table.Column<string>(nullable: true),
                    GitisReference = table.Column<string>(nullable: true),
                    HasCriminalRecord = table.Column<bool>(nullable: false),
                    HowFarQuestion = table.Column<string>(nullable: true),
                    IsDbsChecked = table.Column<bool>(nullable: false),
                    PreferredSubjectId = table.Column<int>(nullable: true),
                    TeacherTrainingStatus = table.Column<int>(nullable: false),
                    WhyQuestion = table.Column<string>(nullable: true),
                    GitisDataId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_Subjects_DegreeSubjectId",
                        column: x => x.DegreeSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidates_GitisUsers_GitisDataId",
                        column: x => x.GitisDataId,
                        principalTable: "GitisUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidates_Subjects_PreferredSubjectId",
                        column: x => x.PreferredSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchoolId = table.Column<int>(nullable: true),
                    ReportTo = table.Column<string>(nullable: true),
                    BookedDate = table.Column<TimeSpan>(nullable: true),
                    ExtraNotes = table.Column<string>(nullable: true),
                    RequestedDate = table.Column<DateTime>(nullable: true),
                    RespondBy = table.Column<DateTime>(nullable: false),
                    CandidateId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    RequestedSubjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Subjects_RequestedSubjectId",
                        column: x => x.RequestedSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CandidateCalendar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Start = table.Column<DateTime>(nullable: false),
                    Finish = table.Column<DateTime>(nullable: false),
                    CandidateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCalendar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateCalendar_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CandidateId",
                table: "Bookings",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RequestedSubjectId",
                table: "Bookings",
                column: "RequestedSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SchoolId",
                table: "Bookings",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCalendar_CandidateId",
                table: "CandidateCalendar",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_DegreeSubjectId",
                table: "Candidates",
                column: "DegreeSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_GitisDataId",
                table: "Candidates",
                column: "GitisDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PreferredSubjectId",
                table: "Candidates",
                column: "PreferredSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolCalendar_SchoolId",
                table: "SchoolCalendar",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSchoolUserJoin_SchoolUserId",
                table: "SchoolSchoolUserJoin",
                column: "SchoolUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SchoolId",
                table: "Subjects",
                column: "SchoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "CandidateCalendar");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "SchoolCalendar");

            migrationBuilder.DropTable(
                name: "SchoolSchoolUserJoin");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "SchoolUsers");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "GitisUsers");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
