using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_124 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Worker_AssignedToId",
                table: "Activity");

            migrationBuilder.DropIndex(
                name: "IX_Activity_AssignedToId",
                table: "Activity");

            migrationBuilder.AddColumn<int>(
                name: "AssignedTo",
                table: "Activity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Activity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_AssignedTo",
                table: "Activity",
                column: "AssignedTo");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Worker_AssignedTo",
                table: "Activity",
                column: "AssignedTo",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Worker_AssignedTo",
                table: "Activity");

            migrationBuilder.DropIndex(
                name: "IX_Activity_AssignedTo",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Activity");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_AssignedToId",
                table: "Activity",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Worker_AssignedToId",
                table: "Activity",
                column: "AssignedToId",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
