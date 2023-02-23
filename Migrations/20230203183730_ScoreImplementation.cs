using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sdc.Migrations
{
    /// <inheritdoc />
    public partial class ScoreImplementation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MAT_Shot_x_Round_MAT_Rounds_RoundId",
                table: "MAT_Shot_x_Round");

            migrationBuilder.AlterColumn<int>(
                name: "Rank",
                table: "PLA_Players",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "RankOld",
                table: "PLA_EloHistories",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "RankNew",
                table: "PLA_EloHistories",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "RoundId",
                table: "MAT_Shot_x_Round",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player1Score",
                table: "MAT_Sets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Player2Score",
                table: "MAT_Sets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Player1Score",
                table: "MAT_Matchs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player2Score",
                table: "MAT_Matchs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player1Score",
                table: "MAT_Legs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Player2Score",
                table: "MAT_Legs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MAT_Shot_x_Round_MAT_Rounds_RoundId",
                table: "MAT_Shot_x_Round",
                column: "RoundId",
                principalTable: "MAT_Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MAT_Shot_x_Round_MAT_Rounds_RoundId",
                table: "MAT_Shot_x_Round");

            migrationBuilder.DropColumn(
                name: "Player1Score",
                table: "MAT_Sets");

            migrationBuilder.DropColumn(
                name: "Player2Score",
                table: "MAT_Sets");

            migrationBuilder.DropColumn(
                name: "Player1Score",
                table: "MAT_Matchs");

            migrationBuilder.DropColumn(
                name: "Player2Score",
                table: "MAT_Matchs");

            migrationBuilder.DropColumn(
                name: "Player1Score",
                table: "MAT_Legs");

            migrationBuilder.DropColumn(
                name: "Player2Score",
                table: "MAT_Legs");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rank",
                table: "PLA_Players",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "RankOld",
                table: "PLA_EloHistories",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "RankNew",
                table: "PLA_EloHistories",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RoundId",
                table: "MAT_Shot_x_Round",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MAT_Shot_x_Round_MAT_Rounds_RoundId",
                table: "MAT_Shot_x_Round",
                column: "RoundId",
                principalTable: "MAT_Rounds",
                principalColumn: "Id");
        }
    }
}
