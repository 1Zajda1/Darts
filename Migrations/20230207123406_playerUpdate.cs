using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sdc.Migrations
{
    /// <inheritdoc />
    public partial class playerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MAT_Matchs_PLA_Players_Player2Id",
                table: "MAT_Matchs");

            migrationBuilder.DropForeignKey(
                name: "FK_MAT_Matchs_TUR_Tournaments_TournamentId",
                table: "MAT_Matchs");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "PLA_Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "PLA_Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "TournamentId",
                table: "MAT_Matchs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Player2Id",
                table: "MAT_Matchs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MAT_Matchs_PLA_Players_Player2Id",
                table: "MAT_Matchs",
                column: "Player2Id",
                principalTable: "PLA_Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MAT_Matchs_TUR_Tournaments_TournamentId",
                table: "MAT_Matchs",
                column: "TournamentId",
                principalTable: "TUR_Tournaments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MAT_Matchs_PLA_Players_Player2Id",
                table: "MAT_Matchs");

            migrationBuilder.DropForeignKey(
                name: "FK_MAT_Matchs_TUR_Tournaments_TournamentId",
                table: "MAT_Matchs");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "PLA_Players");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "PLA_Players");

            migrationBuilder.AlterColumn<int>(
                name: "TournamentId",
                table: "MAT_Matchs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Player2Id",
                table: "MAT_Matchs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MAT_Matchs_PLA_Players_Player2Id",
                table: "MAT_Matchs",
                column: "Player2Id",
                principalTable: "PLA_Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MAT_Matchs_TUR_Tournaments_TournamentId",
                table: "MAT_Matchs",
                column: "TournamentId",
                principalTable: "TUR_Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
