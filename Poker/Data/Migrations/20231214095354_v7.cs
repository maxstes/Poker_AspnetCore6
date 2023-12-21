using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poker.Data.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "PlayersOnline");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "PlayersOnline");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "PlayersOnline");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "PlayersOnline",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "PlayersOnline",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Rank",
                table: "PlayersOnline",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
