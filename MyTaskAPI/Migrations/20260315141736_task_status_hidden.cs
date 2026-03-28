using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class task_status_hidden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isHidden",
                table: "TaskStatuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TaskStatuses",
                keyColumn: "id",
                keyValue: 1,
                column: "isHidden",
                value: false);

            migrationBuilder.UpdateData(
                table: "TaskStatuses",
                keyColumn: "id",
                keyValue: 2,
                column: "isHidden",
                value: false);

            migrationBuilder.UpdateData(
                table: "TaskStatuses",
                keyColumn: "id",
                keyValue: 3,
                column: "isHidden",
                value: false);

            migrationBuilder.UpdateData(
                table: "TaskStatuses",
                keyColumn: "id",
                keyValue: 4,
                column: "isHidden",
                value: false);

            migrationBuilder.UpdateData(
                table: "TaskStatuses",
                keyColumn: "id",
                keyValue: 5,
                column: "isHidden",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isHidden",
                table: "TaskStatuses");
        }
    }
}
