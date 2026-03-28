using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyTaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class tasks_type_defaultTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TypeTasks",
                columns: new[] { "id", "color", "name" },
                values: new object[,]
                {
                    { 1, "#CCFFCC", "Personal" },
                    { 2, "#CCE5FF", "Dev" },
                    { 3, "#CCCCFF", "Work" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeTasks",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypeTasks",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TypeTasks",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
