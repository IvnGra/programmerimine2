using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class iuhy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TournamentDescription",
                table: "Tournaments");

            migrationBuilder.RenameColumn(
                name: "TeamNumber",
                table: "Teams",
                newName: "TeamDescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeamDescription",
                table: "Teams",
                newName: "TeamNumber");

            migrationBuilder.AddColumn<string>(
                name: "TournamentDescription",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
