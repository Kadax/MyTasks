using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class default_Statuses_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_statusId",
                table: "Tasks",
                column: "statusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskStatuses_statusId",
                table: "Tasks",
                column: "statusId",
                principalTable: "TaskStatuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskStatuses_statusId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_statusId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
