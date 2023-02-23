using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sdc.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MAT_Shots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: true),
                    IsFilled = table.Column<bool>(type: "bit", nullable: false),
                    IsForClosing = table.Column<bool>(type: "bit", nullable: false),
                    IsClosingDart = table.Column<bool>(type: "bit", nullable: false),
                    IsSkipped = table.Column<bool>(type: "bit", nullable: false),
                    IsDouble = table.Column<bool>(type: "bit", nullable: false),
                    IsTriple = table.Column<bool>(type: "bit", nullable: false),
                    IsOvershot = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAT_Shots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PLA_Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLA_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MAT_Matchs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Player1Id = table.Column<int>(type: "int", nullable: true),
                    Player2Id = table.Column<int>(type: "int", nullable: false),
                    DateTimePlayed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTimeDone = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTimePlaned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTimeCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAT_Matchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MAT_Matchs_PLA_Players_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "PLA_Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MAT_Matchs_PLA_Players_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "PLA_Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PLA_EloHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    RankOld = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    RankNew = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLA_EloHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PLA_EloHistories_PLA_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "PLA_Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MAT_Sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAT_Sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MAT_Sets_MAT_Matchs_MatchId",
                        column: x => x.MatchId,
                        principalTable: "MAT_Matchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MAT_Legs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAT_Legs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MAT_Legs_MAT_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "MAT_Sets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MAT_Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoundCount = table.Column<int>(type: "int", nullable: false),
                    LegId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAT_Rounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MAT_Rounds_MAT_Legs_LegId",
                        column: x => x.LegId,
                        principalTable: "MAT_Legs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MAT_Rounds_PLA_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "PLA_Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MAT_Shot_x_Round",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShotCount = table.Column<int>(type: "int", nullable: false),
                    ShotId = table.Column<int>(type: "int", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAT_Shot_x_Round", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MAT_Shot_x_Round_MAT_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "MAT_Rounds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MAT_Shot_x_Round_MAT_Shots_ShotId",
                        column: x => x.ShotId,
                        principalTable: "MAT_Shots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MAT_Legs_SetId",
                table: "MAT_Legs",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_MAT_Matchs_Player1Id",
                table: "MAT_Matchs",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MAT_Matchs_Player2Id",
                table: "MAT_Matchs",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_MAT_Rounds_LegId",
                table: "MAT_Rounds",
                column: "LegId");

            migrationBuilder.CreateIndex(
                name: "IX_MAT_Rounds_PlayerId",
                table: "MAT_Rounds",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MAT_Sets_MatchId",
                table: "MAT_Sets",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MAT_Shot_x_Round_RoundId",
                table: "MAT_Shot_x_Round",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_MAT_Shot_x_Round_ShotId",
                table: "MAT_Shot_x_Round",
                column: "ShotId");

            migrationBuilder.CreateIndex(
                name: "IX_PLA_EloHistories_PlayerId",
                table: "PLA_EloHistories",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MAT_Shot_x_Round");

            migrationBuilder.DropTable(
                name: "PLA_EloHistories");

            migrationBuilder.DropTable(
                name: "MAT_Rounds");

            migrationBuilder.DropTable(
                name: "MAT_Shots");

            migrationBuilder.DropTable(
                name: "MAT_Legs");

            migrationBuilder.DropTable(
                name: "MAT_Sets");

            migrationBuilder.DropTable(
                name: "MAT_Matchs");

            migrationBuilder.DropTable(
                name: "PLA_Players");
        }
    }
}
