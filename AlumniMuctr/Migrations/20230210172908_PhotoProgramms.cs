using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniMuctr.Migrations
{
    public partial class PhotoProgramms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Programms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Programms");
        }
    }
}
