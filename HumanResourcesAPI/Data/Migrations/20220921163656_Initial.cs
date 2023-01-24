using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResourcesAPI.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Birth", "Category", "Description", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, "2000-12-12", "Category 1", "Hello World", "Karlo Bakota", 4 },
                    { 2, "2000-12-12", "Category 1", "Hello World", "Petar Mikulic", 4 },
                    { 3, "2000-12-12", "Category 2", "Hello World", "Tea Mikulic", 4 },
                    { 4, "2000-12-12", "Category 2", "Hello World", "Ivan Mikulic", 4 },
                    { 5, "2000-12-12", "Category 2", "Hello World", "Anamarija Mikulic", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
