using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsLearning.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlsToQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "/assets/images/bengal.png");

           
            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "/assets/images/golden-retriever.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: null);
        }
    }
}