using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    CoverUrl = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Rock" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Metalcore" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "PostHardcore" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Post Rock" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Math rock" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Lofi" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { 7, "Chillhop" });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "CoverUrl", "Description", "GenreId", "Name", "Price" },
                values: new object[] { 2, "", "The album Day X has a conceptual idea. The name of the record means 'End of the World'", 2, "Day x", 49.990000000000002 });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "CoverUrl", "Description", "GenreId", "Name", "Price" },
                values: new object[] { 1, "", "Asking Alexandria is the self-titled fifth studio album by British rock band Asking Alexandria.", 3, "Asking alexandria", 99.989999999999995 });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "CoverUrl", "Description", "GenreId", "Name", "Price" },
                values: new object[] { 3, "", "", 4, "Edge party", 49.990000000000002 });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_GenreId",
                table: "Albums",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
