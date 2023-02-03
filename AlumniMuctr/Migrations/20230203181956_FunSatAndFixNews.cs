using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniMuctr.Migrations
{
    public partial class FunSatAndFixNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "News",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FunSaturdayReg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunSaturdayReg", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FunSaturdayReg");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "News");
        }
    }
}
