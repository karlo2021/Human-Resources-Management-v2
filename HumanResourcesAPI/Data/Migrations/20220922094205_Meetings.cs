using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResourcesAPI.Data.Migrations
{
    public partial class Meetings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterviewDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    EmploymentDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employed = table.Column<bool>(type: "bit", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Meetings",
                columns: new[] { "Id", "Address", "Description", "Employed", "EmploymentDate", "InterviewDate", "Name", "PersonId", "Rating" },
                values: new object[,]
                {
                    { 1, "Location 1", "Bad meeting", false, "NaN", "2022-12-12", "Meeting 1", 1, 2 },
                    { 2, "Location 1", "Good meeting", true, "2022-12-20", "2022-12-15", "Meeting 2", 1, 4 },
                    { 3, "Location 1", "Good meeting", true, "2022-12-13", "2022-12-12", "Meeting 1", 3, 5 },
                    { 4, "Location 1", "Good meeting", true, "2022-12-13", "2022-12-12", "Meeting 1", 4, 3 },
                    { 5, "Location 1", "Good meeting", true, "2022-12-13", "2022-12-12", "Meeting 1", 5, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_PersonId",
                table: "Meetings",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetings");
        }
    }
}
