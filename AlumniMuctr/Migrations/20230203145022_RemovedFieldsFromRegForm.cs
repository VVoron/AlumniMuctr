using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniMuctr.Migrations
{
    public partial class RemovedFieldsFromRegForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLivingPlace",
                table: "RegistrationForm");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "RegistrationForm");

            migrationBuilder.DropColumn(
                name: "ScientificSupervisor",
                table: "RegistrationForm");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "RegistrationForm",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "RegistrationForm",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentLivingPlace",
                table: "RegistrationForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "RegistrationForm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ScientificSupervisor",
                table: "RegistrationForm",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
