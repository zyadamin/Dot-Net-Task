using Microsoft.EntityFrameworkCore.Migrations;

namespace task.Migrations
{
    public partial class DbCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userName = table.Column<string>(nullable: true),
                    firstName = table.Column<string>(nullable: true),
                    fatherName = table.Column<string>(nullable: true),
                    familyName = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    birthdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
