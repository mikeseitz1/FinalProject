using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_451 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Project_ProjectId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Worker_AssignedTo",
                table: "Activity");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Activity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssignedTo",
                table: "Activity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Project_ProjectId",
                table: "Activity",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Worker_AssignedTo",
                table: "Activity",
                column: "AssignedTo",
                principalTable: "Worker",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Project_ProjectId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Worker_AssignedTo",
                table: "Activity");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Activity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedTo",
                table: "Activity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Project_ProjectId",
                table: "Activity",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Worker_AssignedTo",
                table: "Activity",
                column: "AssignedTo",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
