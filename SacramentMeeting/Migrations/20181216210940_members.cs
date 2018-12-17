using Microsoft.EntityFrameworkCore.Migrations;

namespace SacramentMeeting.Migrations
{
    public partial class members : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Member");

            migrationBuilder.AddColumn<int>(
                name: "MembersGender",
                table: "Member",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MembersGender",
                table: "Member");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Member",
                nullable: false,
                defaultValue: 0);
        }
    }
}
