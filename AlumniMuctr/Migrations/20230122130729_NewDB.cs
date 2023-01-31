using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniMuctr.Migrations
{
    public partial class NewDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BriefDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programms", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "RegistrationForm",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FCs = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FCsгUniversity = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Faculty = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ScientificSupervisor = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EndUniversityTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CurrentLivingPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CurrentWorkingPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CurrentPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SignificantAchievements = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        GraduatesOfMUCTRMHTI = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Hobby = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Subscription = table.Column<bool>(type: "bit", nullable: false),
            //        LiveOfAssociation = table.Column<bool>(type: "bit", nullable: false),
            //        FunSaturday = table.Column<bool>(type: "bit", nullable: false),
            //        DataProcessing = table.Column<bool>(type: "bit", nullable: false),
            //        TimeRegistration = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RegistrationForm", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BriefDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_CategoryId",
                table: "News",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Programms");

            //migrationBuilder.DropTable(
            //    name: "RegistrationForm");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
