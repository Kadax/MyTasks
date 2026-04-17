using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class Status_orderNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "orderNumber",
                table: "TaskStatuses",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TaskStatuses",
                keyColumn: "id",
                keyValue: 1,
                column: "orderNumber",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TaskStatuses",
                keyColumn: "id",
                keyValue: 2,
                column: "orderNumber",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TaskStatuses",
                keyColumn: "id",
                keyValue: 3,
                column: "orderNumber",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TaskStatuses",
                keyColumn: "id",
                keyValue: 4,
                column: "orderNumber",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TaskStatuses",
                keyColumn: "id",
                keyValue: 5,
                column: "orderNumber",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "orderNumber",
                table: "TaskStatuses");
        }
    }
}
