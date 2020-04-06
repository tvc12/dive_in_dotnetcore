using Microsoft.EntityFrameworkCore.Migrations;

namespace CatBasicExample.Migrations
{
    public partial class add_cat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cat",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    url = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    age = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cat", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cat");
        }
    }
}
