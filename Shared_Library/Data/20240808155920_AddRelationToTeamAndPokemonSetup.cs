using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared_Library.Data
{
    public partial class AddRelationToTeamAndPokemonSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PokemonSetUps_TeamId",
                table: "PokemonSetUps",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonSetUps_Teams_TeamId",
                table: "PokemonSetUps",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonSetUps_Teams_TeamId",
                table: "PokemonSetUps");

            migrationBuilder.DropIndex(
                name: "IX_PokemonSetUps_TeamId",
                table: "PokemonSetUps");
        }
    }
}
