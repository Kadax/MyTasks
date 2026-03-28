using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class tasks_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "autorId",
                table: "Tasks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "modifiedId",
                table: "Tasks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_autorId",
                table: "Tasks",
                column: "autorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_modifiedId",
                table: "Tasks",
                column: "modifiedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_autorId",
                table: "Tasks",
                column: "autorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_modifiedId",
                table: "Tasks",
                column: "modifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_autorId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_modifiedId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_autorId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_modifiedId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "autorId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "modifiedId",
                table: "Tasks");
        }
    }
}
