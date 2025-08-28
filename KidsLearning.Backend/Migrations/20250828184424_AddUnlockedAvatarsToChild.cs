using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsLearning.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUnlockedAvatarsToChild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Children",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "ChildId",
                table: "Avatars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_ChildId",
                table: "Avatars",
                column: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Children_ChildId",
                table: "Avatars",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Children_ChildId",
                table: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_ChildId",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "Avatars");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Children",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
