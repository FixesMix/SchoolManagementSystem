using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStudentProfessorTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professsors",
                table: "Professsors");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Professsors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Professsors");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Professsors");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Professsors");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Professsors");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Professsors");

            migrationBuilder.RenameTable(
                name: "Professsors",
                newName: "Professors");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Students",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Professors",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Professors",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Courses",
                table: "Professors",
                newName: "TaughtCourses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professors",
                table: "Professors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_Users_UserId",
                table: "Professors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professors_Users_UserId",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professors",
                table: "Professors");

            migrationBuilder.RenameTable(
                name: "Professors",
                newName: "Professsors");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Students",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "TaughtCourses",
                table: "Professsors",
                newName: "Courses");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Professsors",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Professsors",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Students",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Professsors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Professsors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Professsors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Professsors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Professsors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Professsors",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professsors",
                table: "Professsors",
                column: "Id");
        }
    }
}
