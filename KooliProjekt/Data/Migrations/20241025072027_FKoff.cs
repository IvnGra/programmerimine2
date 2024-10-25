using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class FKoff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tournament_name",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "team_name",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "Is_Admin",
                table: "Users",
                newName: "IsAdmin");

            migrationBuilder.RenameColumn(
                name: "Start_datebigit",
                table: "Tournaments",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End_date",
                table: "Tournaments",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "points_earned",
                table: "Predictions",
                newName: "PointsEarned");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TournamentName",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeamName",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Match_time",
                table: "Matchs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TournamentName",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "TeamName",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "IsAdmin",
                table: "Users",
                newName: "Is_Admin");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Tournaments",
                newName: "Start_datebigit");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Tournaments",
                newName: "End_date");

            migrationBuilder.RenameColumn(
                name: "PointsEarned",
                table: "Predictions",
                newName: "points_earned");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Tournament_name",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "team_name",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Match_time",
                table: "Matchs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
