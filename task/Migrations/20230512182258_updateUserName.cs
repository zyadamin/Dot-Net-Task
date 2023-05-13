using Microsoft.EntityFrameworkCore.Migrations;

namespace task.Migrations
{
    public partial class updateUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Persons_userName",
                table: "Persons",
                column: "userName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_userName",
                table: "Persons");
        }
    }
}
