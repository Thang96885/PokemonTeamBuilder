using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared_Library.Data
{
    public partial class DeleteMoveUrlColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Moves_Url",
                table: "Moves");


            migrationBuilder.DropColumn(
                name: "Url",
                table: "Moves");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "PokemonSetUps",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PicUrl",
                table: "PokemonSetUps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Moves",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Moves",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_Url",
                table: "Moves",
                column: "Url",
                unique: true);
        }
    }
}
