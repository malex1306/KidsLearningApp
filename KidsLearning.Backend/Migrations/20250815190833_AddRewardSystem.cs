using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsLearning.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddRewardSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsecutiveLearningDays",
                table: "Children",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DailyLearningMinutes",
                table: "Children",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLearningDay",
                table: "Children",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StarCount",
                table: "Children",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Badge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Badge_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Badge_ChildId",
                table: "Badge",
                column: "ChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Badge");

            migrationBuilder.DropColumn(
                name: "ConsecutiveLearningDays",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "DailyLearningMinutes",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "LastLearningDay",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "StarCount",
                table: "Children");
        }
    }
}
