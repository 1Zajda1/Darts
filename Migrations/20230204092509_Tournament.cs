using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sdc.Migrations
{
    /// <inheritdoc />
    public partial class Tournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "MAT_Matchs",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TUR_Tournament",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPlayers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUR_Tournament", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MAT_Matchs_TournamentId",
                table: "MAT_Matchs",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MAT_Matchs_TUR_Tournament_TournamentId",
                table: "MAT_Matchs",
                column: "TournamentId",
                principalTable: "TUR_Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MAT_Matchs_TUR_Tournament_TournamentId",
                table: "MAT_Matchs");

            migrationBuilder.DropTable(
                name: "TUR_Tournament");

            migrationBuilder.DropIndex(
                name: "IX_MAT_Matchs_TournamentId",
                table: "MAT_Matchs");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "MAT_Matchs");
        }
    }
}
