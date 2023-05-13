using Microsoft.EntityFrameworkCore.Migrations;

namespace task.Migrations
{
    public partial class updatNewPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "newPassword",
                table: "Persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "newPassword",
                table: "Persons");
        }
    }
}
