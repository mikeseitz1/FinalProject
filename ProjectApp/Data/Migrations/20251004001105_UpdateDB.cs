using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Project_ProjectId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Worker_AssignedToId",
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
                name: "AssignedToId",
                table: "Activity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AssignedToEmail",
                table: "Activity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Activity",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Project_ProjectId",
                table: "Activity",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Worker_AssignedToId",
                table: "Activity",
                column: "AssignedToId",
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
                name: "FK_Activity_Worker_AssignedToId",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "AssignedToEmail",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Activity");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Activity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedToId",
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
                name: "FK_Activity_Worker_AssignedToId",
                table: "Activity",
                column: "AssignedToId",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
