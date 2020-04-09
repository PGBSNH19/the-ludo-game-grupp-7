using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoGameEngine.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GamePiece_PlayerID",
                table: "GamePiece",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePiece_Player_PlayerID",
                table: "GamePiece",
                column: "PlayerID",
                principalTable: "Player",
                principalColumn: "PlayerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePiece_Player_PlayerID",
                table: "GamePiece");

            migrationBuilder.DropIndex(
                name: "IX_GamePiece_PlayerID",
                table: "GamePiece");
        }
    }
}
