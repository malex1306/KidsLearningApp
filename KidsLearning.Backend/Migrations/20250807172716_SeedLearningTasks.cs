using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsLearning.Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedLearningTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
        table: "LearningTasks",
        columns: new[] { "Id", "Title", "Description", "Subject" },
        values: new object[,]
        {
            { 1, "Zahlen finden", "Finde die fehlenden Zahlen in der Reihe.", "Mathe-Abenteuer" },
            { 2, "Addition bis 10", "Addiere zwei einstellige Zahlen.", "Mathe-Abenteuer" },
            { 3, "Formen erkennen", "Erkenne verschiedene geometrische Formen.", "Mathe-Abenteuer" }
        });

    
    migrationBuilder.InsertData(
        table: "LearningTasks",
        columns: new[] { "Id", "Title", "Description", "Subject" },
        values: new object[,]
        {
            { 4, "Alphabet lernen", "Nenne alle Buchstaben des Alphabets in der richtigen Reihenfolge.", "Buchstaben-land" },
            { 5, "Buchstaben verbinden", "Verbinde die Großbuchstaben mit den Kleinbuchstaben.", "Buchstaben-land" },
            { 6, "Wörter buchstabieren", "Buchstabiere einfache Wörter wie 'Hund' und 'Katze'.", "Buchstaben-land" }
        });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
        table: "LearningTasks",
        keyColumn: "Id",
        keyValues: new object[] { 1, 2, 3, 4, 5, 6 });
        }
    }
}
