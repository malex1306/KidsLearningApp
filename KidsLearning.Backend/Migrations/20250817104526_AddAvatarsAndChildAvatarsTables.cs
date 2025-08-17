using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsLearning.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatarsAndChildAvatarsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Erstellt die Avatars-Tabelle mit der korrekten Struktur
            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnlockStarRequirement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            // Erstellt eine neue Tabelle, um die Beziehung zwischen Kindern und Avataren zu speichern
            migrationBuilder.CreateTable(
                name: "ChildAvatars",
                columns: table => new
                {
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    AvatarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildAvatars", x => new { x.ChildId, x.AvatarId });
                    table.ForeignKey(
                        name: "FK_ChildAvatars_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChildAvatars_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            // Fügt einige Beispieldaten in die Avatars-Tabelle ein
            migrationBuilder.InsertData(
                table: "Avatars",
                columns: new[] { "Id", "Name", "ImageUrl", "Description", "UnlockStarRequirement" },
                values: new object[,]
                {
                    { 1, "Glückliche Ente", "assets/images/duck.png", "Eine fröhliche Ente, die gerne schwimmt.", 2 },
                    { 2, "Abenteuerlicher Bär", "assets/images/bear.png", "Ein mutiger Bär, der gerne im Wald spielt.", 4 },
                    { 3, "Clevere Eule", "assets/images/owl.png", "Eine weise Eule, die alle Antworten kennt.", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildAvatars_AvatarId",
                table: "ChildAvatars",
                column: "AvatarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildAvatars");

            migrationBuilder.DropTable(
                name: "Avatars");
        }
    }
}