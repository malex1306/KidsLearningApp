using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsLearning.Backend.Migrations
{
    /// <inheritdoc />
    public partial class fixAgeLogicChild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Children");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Children",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Children");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Children",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
