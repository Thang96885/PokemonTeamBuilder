using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared_Library.Data
{
    public partial class UpdateDatabaseRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_PokemonSetUps_PokemonSetUpId",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_PokemonSetUpId",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "PokemonSetUpId",
                table: "Types");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Types",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Moves",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "PokemonSetUpTypeDto",
                columns: table => new
                {
                    PokemonsHaveTypeId = table.Column<int>(type: "int", nullable: false),
                    TypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSetUpTypeDto", x => new { x.PokemonsHaveTypeId, x.TypesId });
                    table.ForeignKey(
                        name: "FK_PokemonSetUpTypeDto_PokemonSetUps_PokemonsHaveTypeId",
                        column: x => x.PokemonsHaveTypeId,
                        principalTable: "PokemonSetUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonSetUpTypeDto_Types_TypesId",
                        column: x => x.TypesId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Types_Name",
                table: "Types",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Moves_Url",
                table: "Moves",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSetUpTypeDto_TypesId",
                table: "PokemonSetUpTypeDto",
                column: "TypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonSetUpTypeDto");

            migrationBuilder.DropIndex(
                name: "IX_Types_Name",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Moves_Url",
                table: "Moves");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Types",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "PokemonSetUpId",
                table: "Types",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Moves",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Types_PokemonSetUpId",
                table: "Types",
                column: "PokemonSetUpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_PokemonSetUps_PokemonSetUpId",
                table: "Types",
                column: "PokemonSetUpId",
                principalTable: "PokemonSetUps",
                principalColumn: "Id");
        }
    }
}
