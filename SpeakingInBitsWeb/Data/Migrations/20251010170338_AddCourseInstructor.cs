using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpeakingInBitsWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseInstructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseInstructorId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseInstructorId",
                table: "Courses",
                column: "CourseInstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_CourseInstructorId",
                table: "Courses",
                column: "CourseInstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_CourseInstructorId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseInstructorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseInstructorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
