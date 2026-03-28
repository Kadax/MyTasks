using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class tasks_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isFixed",
                table: "Tasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "taskTypesId",
                table: "Tasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeTasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTasks", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_taskTypesId",
                table: "Tasks",
                column: "taskTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TypeTasks_taskTypesId",
                table: "Tasks",
                column: "taskTypesId",
                principalTable: "TypeTasks",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TypeTasks_taskTypesId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "TypeTasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_taskTypesId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "isFixed",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "taskTypesId",
                table: "Tasks");
        }
    }
}
