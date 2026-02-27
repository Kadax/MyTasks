using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class task_time : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createAt",
                table: "TaskStatuses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "TaskStatuses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "createAt",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "totalTime",
                table: "Tasks",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "createAt",
                table: "Executors",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "Executors",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TimeSpents",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    time = table.Column<double>(type: "REAL", nullable: false),
                    taskId = table.Column<int>(type: "INTEGER", nullable: false),
                    createAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updateAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSpents", x => x.id);
                    table.ForeignKey(
                        name: "FK_TimeSpents_Tasks_taskId",
                        column: x => x.taskId,
                        principalTable: "Tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSpents_taskId",
                table: "TimeSpents",
                column: "taskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSpents");

            migrationBuilder.DropColumn(
                name: "createAt",
                table: "TaskStatuses");

            migrationBuilder.DropColumn(
                name: "updateAt",
                table: "TaskStatuses");

            migrationBuilder.DropColumn(
                name: "createAt",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "totalTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "updateAt",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "createAt",
                table: "Executors");

            migrationBuilder.DropColumn(
                name: "updateAt",
                table: "Executors");
        }
    }
}
