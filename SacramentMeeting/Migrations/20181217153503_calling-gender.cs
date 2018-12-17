using Microsoft.EntityFrameworkCore.Migrations;

namespace SacramentMeeting.Migrations
{
    public partial class callinggender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CallingGender",
                table: "CurrentCalling",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallingGender",
                table: "CurrentCalling");
        }
    }
}
