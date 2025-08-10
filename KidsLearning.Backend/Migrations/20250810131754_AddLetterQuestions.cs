using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsLearning.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddLetterQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Text", "CorrectAnswer", "Options", "LearningTaskId" },
                values: new object[,]
                {
                    // Fragen für "Alphabet lernen" (LearningTask 4)
                    { 7, "Welcher Buchstabe kommt nach dem B?", "C", "A;B;C;D", 4 },
                    { 8, "Welcher Buchstabe kommt vor dem G?", "F", "D;E;F;H", 4 },

                    // Fragen für "Buchstaben verbinden" (LearningTask 5)
                    { 9, "Welche Kombination ist richtig?", "A-a", "A-b;A-c;A-a;A-d", 5 },
                    { 10, "Welche Kombination ist richtig?", "E-e", "E-f;E-g;E-h;E-e", 5 },

                    // Fragen für "Wörter buchstabieren" (LearningTask 6)
                    { 11, "Buchstabiere das Wort 'Katze'.", "K-A-T-Z-E", "K-A-Z-E;K-A-T-Z-E;K-A-Z-E-N;K-A-T-S-E", 6 },
                    { 12, "Buchstabiere das Wort 'Hund'.", "H-U-N-D", "H-U-N-D;H-U-N-S;H-O-N-D;H-U-N-T", 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValues: new object[] { 7, 8, 9, 10, 11, 12 });
        }
    }
}