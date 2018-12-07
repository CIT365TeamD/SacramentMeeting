using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SacramentMeeting.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calling",
                columns: table => new
                {
                    CallingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Organization = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calling", x => x.CallingID);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    SongID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.SongID);
                });

            migrationBuilder.CreateTable(
                name: "Meeting",
                columns: table => new
                {
                    MeetingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MeetingDate = table.Column<DateTime>(nullable: false),
                    CallingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => x.MeetingID);
                    table.ForeignKey(
                        name: "FK_Meeting_Calling_CallingID",
                        column: x => x.CallingID,
                        principalTable: "Calling",
                        principalColumn: "CallingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentCalling",
                columns: table => new
                {
                    CurrentCallingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CallingID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false),
                    DateCalled = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentCalling", x => x.CurrentCallingID);
                    table.ForeignKey(
                        name: "FK_CurrentCalling_Calling_CallingID",
                        column: x => x.CallingID,
                        principalTable: "Calling",
                        principalColumn: "CallingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrentCalling_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prayer",
                columns: table => new
                {
                    PrayerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MemberID = table.Column<int>(nullable: false),
                    MeetingID = table.Column<int>(nullable: false),
                    Schedule = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prayer", x => x.PrayerID);
                    table.ForeignKey(
                        name: "FK_Prayer_Meeting_MeetingID",
                        column: x => x.MeetingID,
                        principalTable: "Meeting",
                        principalColumn: "MeetingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prayer_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongSelection",
                columns: table => new
                {
                    SongSelectionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SongID = table.Column<int>(nullable: false),
                    MeetingID = table.Column<int>(nullable: false),
                    Schedule = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongSelection", x => x.SongSelectionID);
                    table.ForeignKey(
                        name: "FK_SongSelection_Meeting_MeetingID",
                        column: x => x.MeetingID,
                        principalTable: "Meeting",
                        principalColumn: "MeetingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongSelection_Song_SongID",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Talk",
                columns: table => new
                {
                    TalkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MeetingID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false),
                    Topic = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talk", x => x.TalkID);
                    table.ForeignKey(
                        name: "FK_Talk_Meeting_MeetingID",
                        column: x => x.MeetingID,
                        principalTable: "Meeting",
                        principalColumn: "MeetingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Talk_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentCalling_CallingID",
                table: "CurrentCalling",
                column: "CallingID");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentCalling_MemberID",
                table: "CurrentCalling",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_CallingID",
                table: "Meeting",
                column: "CallingID");

            migrationBuilder.CreateIndex(
                name: "IX_Prayer_MeetingID",
                table: "Prayer",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_Prayer_MemberID",
                table: "Prayer",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_SongSelection_MeetingID",
                table: "SongSelection",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_SongSelection_SongID",
                table: "SongSelection",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_Talk_MeetingID",
                table: "Talk",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_Talk_MemberID",
                table: "Talk",
                column: "MemberID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentCalling");

            migrationBuilder.DropTable(
                name: "Prayer");

            migrationBuilder.DropTable(
                name: "SongSelection");

            migrationBuilder.DropTable(
                name: "Talk");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "Meeting");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Calling");
        }
    }
}
