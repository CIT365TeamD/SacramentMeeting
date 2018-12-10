using Microsoft.EntityFrameworkCore.Migrations;

namespace SacramentMeeting.Migrations
{
    public partial class tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Member_FirstName_LastName",
                table: "Member",
                columns: new[] { "FirstName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_MeetingDate",
                table: "Meeting",
                column: "MeetingDate",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Member_FirstName_LastName",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Meeting_MeetingDate",
                table: "Meeting");
        }
    }
}
