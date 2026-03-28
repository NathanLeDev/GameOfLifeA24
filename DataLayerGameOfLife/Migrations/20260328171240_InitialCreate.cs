using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayerGameOfLife.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InitialStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitialStates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "InitialStates",
                columns: new[] { "Id", "Name", "State" },
                values: new object[,]
                {
                    { 1, "Blinker", "1,2;2,2;3,2;" },
                    { 2, "Block", "1,1;1,2;2,1;2,2;" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InitialStates_Name",
                table: "InitialStates",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InitialStates");
        }
    }
}
