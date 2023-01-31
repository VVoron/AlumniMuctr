using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniMuctr.Migrations
{
    public partial class ChangeRegFormIdToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrationForm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FCs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FCsгUniversity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Faculty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScientificSupervisor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndUniversityTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentLivingPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentWorkingPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignificantAchievements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduatesOfMUCTRMHTI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hobby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subscription = table.Column<bool>(type: "bit", nullable: false),
                    LiveOfAssociation = table.Column<bool>(type: "bit", nullable: false),
                    FunSaturday = table.Column<bool>(type: "bit", nullable: false),
                    DataProcessing = table.Column<bool>(type: "bit", nullable: false),
                    TimeRegistration = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationForm", x => x.Id);
                });

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "RegistrationForm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Faculty",
                table: "RegistrationForm",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "RegistrationForm",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "RegistrationForm",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationForm");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "RegistrationForm",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
               name: "Faculty",
               table: "RegistrationForm",
               type: "nvarchar(max)",
               nullable: false,
               defaultValue: "",
               oldClrType: typeof(string),
               oldType: "nvarchar(max)",
               oldNullable: true);

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "RegistrationForm");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "RegistrationForm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
