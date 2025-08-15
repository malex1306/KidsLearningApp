using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsLearning.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatarsAndUnlocked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatar_Children_ChildId",
                table: "Avatar");

            migrationBuilder.DropForeignKey(
                name: "FK_Badge_Children_ChildId",
                table: "Badge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Badge",
                table: "Badge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avatar",
                table: "Avatar");

            migrationBuilder.RenameTable(
                name: "Badge",
                newName: "Badges");

            migrationBuilder.RenameTable(
                name: "Avatar",
                newName: "Avatars");

            migrationBuilder.RenameIndex(
                name: "IX_Badge_ChildId",
                table: "Badges",
                newName: "IX_Badges_ChildId");

            migrationBuilder.RenameIndex(
                name: "IX_Avatar_ChildId",
                table: "Avatars",
                newName: "IX_Avatars_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Badges",
                table: "Badges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avatars",
                table: "Avatars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Children_ChildId",
                table: "Avatars",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Badges_Children_ChildId",
                table: "Badges",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Children_ChildId",
                table: "Avatars");

            migrationBuilder.DropForeignKey(
                name: "FK_Badges_Children_ChildId",
                table: "Badges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Badges",
                table: "Badges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avatars",
                table: "Avatars");

            migrationBuilder.RenameTable(
                name: "Badges",
                newName: "Badge");

            migrationBuilder.RenameTable(
                name: "Avatars",
                newName: "Avatar");

            migrationBuilder.RenameIndex(
                name: "IX_Badges_ChildId",
                table: "Badge",
                newName: "IX_Badge_ChildId");

            migrationBuilder.RenameIndex(
                name: "IX_Avatars_ChildId",
                table: "Avatar",
                newName: "IX_Avatar_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Badge",
                table: "Badge",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avatar",
                table: "Avatar",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatar_Children_ChildId",
                table: "Avatar",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Badge_Children_ChildId",
                table: "Badge",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
