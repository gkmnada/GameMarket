using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable_MyGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyGames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyGames_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyGames_GameID",
                table: "MyGames",
                column: "GameID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyGames");
        }
    }
}
