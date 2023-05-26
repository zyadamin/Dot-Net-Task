using Microsoft.EntityFrameworkCore.Migrations;

namespace task.Migrations
{
    public partial class deletecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "newPassword",
                table: "Persons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "newPassword",
                table: "Persons",
                nullable: true);
        }
    }
}
