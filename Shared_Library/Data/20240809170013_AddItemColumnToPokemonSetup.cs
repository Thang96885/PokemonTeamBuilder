using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared_Library.Data
{
    public partial class AddItemColumnToPokemonSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "PokemonSetUps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "PokemonSetUps");
        }
    }
}
