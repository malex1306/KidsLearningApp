using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsLearning.Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSpellingTaskQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Aktualisiert die Daten für Frage 11 (Katze)
            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CorrectAnswer", "Options", "imageUrl" },
                values: new object[] { "KATZE", "K;A;T;Z;E;S;N;P", "assets/images/katze.png" });

            // Aktualisiert die Daten für Frage 12 (Hund)
            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CorrectAnswer", "Options", "imageUrl" },
                values: new object[] { "HUND", "H;U;N;D;T;P;B;L", "assets/images/hund.png" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Setzt die Daten bei einem Rollback auf die ursprünglichen Werte zurück
            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CorrectAnswer", "Options", "imageUrl" },
                values: new object[] { "K-A-T-Z-E", "K-A-Z-E;K-A-T-Z-E;K-A-Z-E-N;K-A-T-S-E", null });
            
            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CorrectAnswer", "Options", "imageUrl" },
                values: new object[] { "H-U-N-D", "H-U-N-D;H-U-N-S;H-O-N-D;H-U-N-T", null });
        }
    }
}