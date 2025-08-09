using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsLearning.Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Questions",
            columns: new[] { "Id", "Text", "CorrectAnswer", "Options", "LearningTaskId" },
            values: new object[,]
    {
        // Fragen für "Zahlen finden" (LearningTask 1)
        { 1, "Welche Zahl fehlt in der Reihe: 1, 2, ?, 4, 5", "3", "1;2;3;4", 1 },
        { 2, "Welche Zahl fehlt in der Reihe: 5, ?, 7, 8, 9", "6", "5;6;7;8", 1 },
        // Fragen für "Addition bis 10" (LearningTask 2)
        { 3, "Was ist 3 + 4?", "7", "5;6;7;8", 2 },
        { 4, "Was ist 5 + 2?", "7", "6;7;8;9", 2 },
        // Fragen für "Formen erkennen" (LearningTask 3)
        { 5, "Welche Form hat vier gleich lange Seiten?", "Quadrat", "Dreieck;Kreis;Quadrat;Rechteck", 3 },
        { 6, "Welche Form hat drei Ecken?", "Dreieck", "Quadrat;Dreieck;Kreis;Stern", 3 }
    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.DeleteData(
        table: "Questions",
        keyColumn: "Id",
        keyValues: new object[] { 1, 2, 3, 4, 5, 6 });
        }
    }
}
