using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBehaviors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matchs_Teams_Team1Id",
                table: "Matchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Matchs_Teams_Team2Id",
                table: "Matchs");

            migrationBuilder.AddForeignKey(
                name: "FK_Matchs_Teams_Team1Id",
                table: "Matchs",
                column: "Team1Id",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matchs_Teams_Team2Id",
                table: "Matchs",
                column: "Team2Id",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matchs_Teams_Team1Id",
                table: "Matchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Matchs_Teams_Team2Id",
                table: "Matchs");

            migrationBuilder.AddForeignKey(
                name: "FK_Matchs_Teams_Team1Id",
                table: "Matchs",
                column: "Team1Id",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matchs_Teams_Team2Id",
                table: "Matchs",
                column: "Team2Id",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
