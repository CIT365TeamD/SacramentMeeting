using Microsoft.EntityFrameworkCore.Migrations;

namespace SacramentMeeting.Migrations
{
    public partial class ComplexModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Topic",
                table: "Talk",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Topic",
                table: "Talk",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
