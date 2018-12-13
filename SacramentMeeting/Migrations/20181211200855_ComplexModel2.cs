using Microsoft.EntityFrameworkCore.Migrations;

namespace SacramentMeeting.Migrations
{
    public partial class ComplexModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SongSelection_MeetingID",
                table: "SongSelection");

            migrationBuilder.DropIndex(
                name: "IX_Prayer_MeetingID",
                table: "Prayer");

            migrationBuilder.CreateIndex(
                name: "IX_SongSelection_MeetingID_Schedule",
                table: "SongSelection",
                columns: new[] { "MeetingID", "Schedule" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prayer_MeetingID_Schedule",
                table: "Prayer",
                columns: new[] { "MeetingID", "Schedule" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SongSelection_MeetingID_Schedule",
                table: "SongSelection");

            migrationBuilder.DropIndex(
                name: "IX_Prayer_MeetingID_Schedule",
                table: "Prayer");

            migrationBuilder.CreateIndex(
                name: "IX_SongSelection_MeetingID",
                table: "SongSelection",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_Prayer_MeetingID",
                table: "Prayer",
                column: "MeetingID");
        }
    }
}
