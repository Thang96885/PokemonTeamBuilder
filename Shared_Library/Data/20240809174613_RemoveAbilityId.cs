using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared_Library.Data
{
    public partial class RemoveAbilityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbilityId",
                table: "PokemonSetUps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbilityId",
                table: "PokemonSetUps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
