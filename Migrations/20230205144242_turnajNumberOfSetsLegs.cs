using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sdc.Migrations
{
    /// <inheritdoc />
    public partial class turnajNumberOfSetsLegs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FullScore",
                table: "TUR_Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLegs",
                table: "TUR_Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSets",
                table: "TUR_Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FullScore",
                table: "MAT_Matchs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLegs",
                table: "MAT_Matchs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSets",
                table: "MAT_Matchs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullScore",
                table: "TUR_Tournaments");

            migrationBuilder.DropColumn(
                name: "NumberOfLegs",
                table: "TUR_Tournaments");

            migrationBuilder.DropColumn(
                name: "NumberOfSets",
                table: "TUR_Tournaments");

            migrationBuilder.DropColumn(
                name: "FullScore",
                table: "MAT_Matchs");

            migrationBuilder.DropColumn(
                name: "NumberOfLegs",
                table: "MAT_Matchs");

            migrationBuilder.DropColumn(
                name: "NumberOfSets",
                table: "MAT_Matchs");
        }
    }
}
