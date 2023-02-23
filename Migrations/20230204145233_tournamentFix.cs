using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sdc.Migrations
{
    /// <inheritdoc />
    public partial class tournamentFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MAT_Matchs_TUR_Tournament_TournamentId",
                table: "MAT_Matchs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUR_Tournament",
                table: "TUR_Tournament");

            migrationBuilder.RenameTable(
                name: "TUR_Tournament",
                newName: "TUR_Tournaments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUR_Tournaments",
                table: "TUR_Tournaments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MAT_Matchs_TUR_Tournaments_TournamentId",
                table: "MAT_Matchs",
                column: "TournamentId",
                principalTable: "TUR_Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MAT_Matchs_TUR_Tournaments_TournamentId",
                table: "MAT_Matchs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUR_Tournaments",
                table: "TUR_Tournaments");

            migrationBuilder.RenameTable(
                name: "TUR_Tournaments",
                newName: "TUR_Tournament");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUR_Tournament",
                table: "TUR_Tournament",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MAT_Matchs_TUR_Tournament_TournamentId",
                table: "MAT_Matchs",
                column: "TournamentId",
                principalTable: "TUR_Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
