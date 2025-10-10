using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpeakingInBitsWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class InstructorOfficeHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfficeHours",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficeHours",
                table: "AspNetUsers");
        }
    }
}
